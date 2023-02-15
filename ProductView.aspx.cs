using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using BCrypt.Net;
namespace TechStore
{

    public partial class ProductView : System.Web.UI.Page
    {
        String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ProductID"] != null)
            {
                if (!IsPostBack)
                {
                    BindProductImages();
                    BindProductDetails();
                    Get_DisplayStars();
                    if (Session["UserID"] != null)
                    {
                        AddReview.Visible = true;
                    }
                    else
                    {
                        AddReview.Visible = false;
                    }
                }
            }
            else
            {
                Response.Redirect("Products");
            }
        }
        private void BindProductImages()
        {
            Int32 ProductID = Convert.ToInt32(Request.QueryString["ProductID"]);
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("select * from ProductImages where ProductID=" + ProductID + "", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dtproddetail = new DataTable();
                        sda.Fill(dtproddetail);
                        rptrImages.DataSource = dtproddetail;
                        rptrImages.DataBind();
                    }
                }
            }
        }
        private void BindProductDetails()
        {
            Int32 ProductID = Convert.ToInt32(Request.QueryString["ProductID"]);
            string ProdInStock = "";
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("select * from Products where ProductID=" + ProductID + "", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dtproddetail = new DataTable();
                        sda.Fill(dtproddetail);
                        if (dtproddetail.Rows.Count != 0)
                        {
                            rptrProductDetails.DataSource = dtproddetail;
                            rptrProductDetails.DataBind();
                            ProdInStock = dtproddetail.Rows[0][3].ToString();
                        }

                        foreach (RepeaterItem item in rptrProductDetails.Items)
                        {
                            var lblavailablestock = item.FindControl("availablestock") as Label;
                            lblavailablestock.Text = "In Stock: " + ProdInStock;
                        }
                    }
                }
            }
        }
        public void Get_DisplayStars()
        {
            Int32 ProductID = Convert.ToInt32(Request.QueryString["ProductID"]);
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT ISNULL(AVG(rating), 0) AverageRating FROM RATINGS WHERE ProductID=@ProdID", con);
            cmd.Parameters.AddWithValue("@ProdID", ProductID);
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            int filledstars = Convert.ToInt32(dt.Rows[0][0].ToString());
            //System.Diagnostics.Debug.WriteLine(filledstars);
            foreach (RepeaterItem item in rptrProductDetails.Items)
            {
                var TopPageRating = item.FindControl("TopPageRating") as Rating;
                TopPageRating.CurrentRating = filledstars;
            }

            //Display Stars for each user (In the Comments/Review Section)
            SqlCommand cmd2 = new SqlCommand("SELECT rating, review, Username, U.UserID, ProfilePicName, ProfilePicExtension FROM RATINGS R INNER JOIN Users U ON R.UserID = U.UserID WHERE ProductID=@ProdID", con);
            cmd2.Parameters.AddWithValue("@ProdID", ProductID);
            DataTable dtUserReview = new DataTable();
            SqlDataAdapter sdaUserReview = new SqlDataAdapter(cmd2);
            sdaUserReview.Fill(dtUserReview);
            if (dtUserReview.Rows.Count != 0)
            {
                rptr_ProdReviews.DataSource = dtUserReview;
                rptr_ProdReviews.DataBind();
            }

        }

        protected string GetActiveClass(int ItemIndex) //Get the "Active" index of the Carousel
        {
            if (ItemIndex == 0)
            {
                return "active"; //This means that this image is displayed on the Carousel
            }
            else
            {
                return "";
            }
        }
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] != null) //Checks if user is Signed In
            {
                int ChosenQuantity = 0; //TextBox value that indicates the customer quantity (CartProdQuantity)
                double ProdSellPrice = 0; //Comes from DB, the selling price that is used for calculation (Checkout/Cart) (ProdSellPrice)
                int ProdAvailableQuan = 0; //Comes from DB to ensure Chosen Quantity does NOT Exceed Available Quantity
                double ProdBuyPrice = 0; //The total price of EACH product (Product Selling Price * ChosenQuantity) (CartProdPrice)
                string userid = Session["UserID"].ToString(); // Get the current UserID --> User MUST BE SIGNED IN  (UserID)
                Int32 ProductID = Convert.ToInt32(Request.QueryString["ProductID"]); //Get the Current ProductID from Query String (ProductID)

                SqlConnection con = new SqlConnection(CS);
                SqlCommand cmd = new SqlCommand("procGetProdDetailsOnID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dtprod = new DataTable();
                sda.Fill(dtprod);

                if (dtprod.Rows.Count != 0)
                {
                    ProdSellPrice = Convert.ToDouble(dtprod.Rows[0][7].ToString()); //Get the Selling Product Price
                                                                                    //System.Diagnostics.Debug.WriteLine("Selling Price: " + ProdSellPrice.ToString();
                    ProdAvailableQuan = Convert.ToInt32(dtprod.Rows[0][3].ToString());
                }

                foreach (RepeaterItem item in rptrProductDetails.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        var lblError = item.FindControl("lblError") as Label;
                        lblError.Text = "";
                        var tb_ProdQuan = item.FindControl("ProdQuan") as TextBox;
                        try
                        {
                            ChosenQuantity = Convert.ToInt32(tb_ProdQuan.Text);
                        }
                        catch
                        {
                            lblError.Text = "";
                            lblError.Text = "Quantity Must Be An Number";
                        }
                    }
                }
                if (ChosenQuantity > 0 && ChosenQuantity < 100 && ChosenQuantity < ProdAvailableQuan)
                {
                    Int32[] ProdIDInCart = new Int32[8];
                    SqlCommand cmd4 = new SqlCommand("SELECT ProductID FROM Cart WHERE UserID=" + userid + "", con);
                    DataTable dtprodincarts = new DataTable();
                    SqlDataAdapter sda4 = new SqlDataAdapter(cmd4);
                    sda4.Fill(dtprodincarts);
                    for (int prodid = 0; prodid < dtprodincarts.Rows.Count; prodid++)
                    {
                        ProdIDInCart[prodid] = Convert.ToInt32(dtprodincarts.Rows[prodid][0].ToString());

                    }
                    for (int prodidd = 0; prodidd < ProdIDInCart.Length; prodidd++)
                    {
                        System.Diagnostics.Debug.WriteLine("Prod ID: " + ProdIDInCart[prodidd]);
                    }
                    ProdBuyPrice = ProdSellPrice * ChosenQuantity;

                    if (ProdIDInCart.Contains(ProductID)) //If item is already in Cart
                    {
                        //Update Cart Quantity and CartProdPrice based on UserID and ProductID
                        //Update Cart Quantity by adding Chosen Quantity and Quantity ALREADY in Cart. Ensure < ProdAvailableQuan
                        //Get ProdQuan in Cart
                        SqlCommand cmd5 = new SqlCommand("SELECT CartProdQuantity FROM Cart WHERE ProductID =" + ProductID + " AND UserID =" + userid + " ", con);
                        SqlDataAdapter sda5 = new SqlDataAdapter(cmd5);
                        DataTable dtCartProdQuan = new DataTable();
                        sda5.Fill(dtCartProdQuan);
                        Int32 QuanInCart = 0;
                        if (dtCartProdQuan.Rows.Count > 0)
                        {
                            QuanInCart = Convert.ToInt32(dtCartProdQuan.Rows[0][0].ToString());
                        }
                        Int32 NewCartProdQuan = ChosenQuantity + QuanInCart;
                        double NewCartPrice = NewCartProdQuan * ProdSellPrice;
                        ProdAvailableQuan -= ChosenQuantity;
                        //Update Cart
                        SqlCommand updateCart = new SqlCommand("UPDATE Cart SET CartProdQuantity = " + NewCartProdQuan + ", CartProdPrice = " + NewCartPrice + " WHERE ProductID= " + ProductID + "", con);
                        SqlCommand updateprod = new SqlCommand("UPDATE Products SET ProdQuantity = " + ProdAvailableQuan.ToString() + " WHERE ProductID=" + ProductID + "", con);
                        con.Open();
                        updateCart.ExecuteNonQuery();
                        updateprod.ExecuteNonQuery();
                    }
                    else //Item is not in Cart
                    {
                        SqlCommand cmd1 = new SqlCommand("INSERT INTO Cart VALUES('" + ChosenQuantity.ToString() + "','" + ProdSellPrice.ToString() + "','" + ProdBuyPrice.ToString() + "','" + userid.ToString() + "','" + ProductID.ToString() + "','Bad')", con); ;
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        ProdAvailableQuan -= ChosenQuantity;
                        SqlCommand cmdUpdateProductsAvailableQuantity = new SqlCommand("UPDATE Products SET ProdQuantity = " + ProdAvailableQuan.ToString() + " WHERE ProductID=" + ProductID + "", con);
                        cmdUpdateProductsAvailableQuantity.ExecuteNonQuery();
                        Response.Redirect("Products");
                    }
                }
                else //This means error in choosing quantity
                {
                    foreach (RepeaterItem item in rptrProductDetails.Items)
                    {
                        if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                        {

                            var lblError = item.FindControl("lblError") as Label;
                            lblError.Text = "";
                            if (ProdAvailableQuan > 1)
                            {
                                lblError.Text = "Quantity must be between 1 - " + ProdAvailableQuan.ToString();
                            }
                            else if (ProdAvailableQuan == 1)
                            {
                                lblError.Text = "This Product Is Out Of Stock";
                            }
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("Home");
            }
        }

        protected void btnPostReview_Click(object sender, EventArgs e)
        {
            Int32 UserID;
            Int32 ProductID = Convert.ToInt32(Request.QueryString["ProductID"].ToString());
            if (Session["UserID"] != null)
            {
                UserID = Convert.ToInt32(Session["UserID"].ToString());
                SqlConnection con = new SqlConnection(CS);
                con.Open();
                //insert rating into database
                SqlCommand cmd = new SqlCommand("INSERT INTO [RATINGS] VALUES (@ratingvalue, @review, @ProdID, @UserID)", con);
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ratingvalue", UserInputRatings.CurrentRating.ToString());
                cmd.Parameters.AddWithValue("@review", txtreview.Text);
                cmd.Parameters.AddWithValue("@ProdID", ProductID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect(Request.RawUrl);
            }

        }
    }
}