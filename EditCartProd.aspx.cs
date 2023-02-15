using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechStore
{
    public partial class EditCartProd : System.Web.UI.Page
    {
        String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null && Request.QueryString["ProductID"] != null)
                {
                    ShowQuanInStock();
                    BindProductImages();
                    BindProductDetails();
                }
                else
                {
                    Response.Redirect("Login");
                }
            }

        }

        private void ShowQuanInStock()
        {
            Int32 ProductID = Convert.ToInt32(Request.QueryString["ProductID"]);
            Int32 UserID = Convert.ToInt32(Session["UserID"]);
            Int32 ProdAvailableQuan = 0; //Comes from DB to ensure Chosen Quantity does NOT Exceed Available Quantity
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("procGetProdDetailsOnID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dtprod = new DataTable();
            sda.Fill(dtprod);

            SqlCommand cmd2 = new SqlCommand("SELECT CartProdQuantity FROM Cart C INNER JOIN Products P On P.ProductID = C.ProductID WHERE P.ProductID = " + ProductID + " AND C.UserID = " + UserID + "", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd2);
            DataTable dtnewquan = new DataTable();
            sda1.Fill(dtnewquan);

            if (dtprod.Rows.Count != 0 && dtnewquan.Rows.Count != 0)
            {
                ProdAvailableQuan = Convert.ToInt32(dtprod.Rows[0][3].ToString()) + Convert.ToInt32(dtnewquan.Rows[0][0].ToString());
            }
            System.Diagnostics.Debug.WriteLine("Available Uqna in label: " + ProdAvailableQuan.ToString());
            availablestock.Text = "In Stock: "+ProdAvailableQuan.ToString();
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
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("select * from Products where ProductID=" + ProductID + "", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dtproddetail = new DataTable();
                        sda.Fill(dtproddetail);
                        rptrProductDetails.DataSource = dtproddetail;
                        rptrProductDetails.DataBind();
                    }

                }
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
       
        protected void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            //Remove the Product From Cart
            Int32 ProductID = Convert.ToInt32(Request.QueryString["ProductID"]);
            Int32 UserID = Convert.ToInt32(Session["UserID"]);
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("procRemoveFromCart", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "DELETE");
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            con.Open();

            //Get the Prod Quantity in Cart
            Int32 ChosenQuantity = 0;
            SqlCommand cmd2 = new SqlCommand("procGetProdChosenQuantity",con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@Action", "SELECT");
            cmd2.Parameters.AddWithValue("@UserID", UserID);
            cmd2.Parameters.AddWithValue("@ProductID", ProductID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd2);
            DataTable dtchosenquan = new DataTable();
            sda.Fill(dtchosenquan);
            if (dtchosenquan.Rows.Count != 0)
            {
                ChosenQuantity = Convert.ToInt32(dtchosenquan.Rows[0][0].ToString());
            }

            //Add the cart quantity back to products since user no longer buying
            SqlCommand cmdupdatequan = new SqlCommand("procUsersideAddQuantity", con);
            cmdupdatequan.CommandType = CommandType.StoredProcedure;
            cmdupdatequan.Parameters.AddWithValue("@Action", "UPDATE");
            cmdupdatequan.Parameters.AddWithValue("@ChosenQuantity",ChosenQuantity);
            cmdupdatequan.Parameters.AddWithValue("@ProductID", ProductID);
            cmdupdatequan.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            Response.Redirect("Cart");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            
            Int32 ProductID = Convert.ToInt32(Request.QueryString["ProductID"]);
            Int32 UserID = Convert.ToInt32(Session["UserID"]);
            Int32 ProdAvailableQuan = 0; //Comes from DB to ensure Chosen Quantity does NOT Exceed Available Quantity
            double ProdSellPrice = 0; //Comes from DB, the selling price that is used for calculation (Checkout/Cart) (ProdSellPrice)
            Int32 NewCartQuan = 0;
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("procGetProdDetailsOnID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dtprod = new DataTable();
            sda.Fill(dtprod);

            SqlCommand cmd2 = new SqlCommand("SELECT CartProdQuantity FROM Cart C INNER JOIN Products P On P.ProductID = C.ProductID WHERE P.ProductID = " + ProductID + " AND C.UserID = " + UserID + "", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd2);
            DataTable dtnewquan = new DataTable();
            sda1.Fill(dtnewquan);

            if (dtprod.Rows.Count != 0 && dtnewquan.Rows.Count!=0)
            {
                ProdSellPrice = Convert.ToDouble(dtprod.Rows[0][7].ToString()); //Get the Selling Product Price
                System.Diagnostics.Debug.WriteLine("dtprod.Rows[0][3].ToString(): " + dtprod.Rows[0][3].ToString());
                System.Diagnostics.Debug.WriteLine("dtnewquan.Rows[0][0].ToString(): " + dtnewquan.Rows[0][0].ToString());
                ProdAvailableQuan = Convert.ToInt32(dtprod.Rows[0][3].ToString()) + Convert.ToInt32(dtnewquan.Rows[0][0].ToString());
            }
            
            System.Diagnostics.Debug.WriteLine("Available Quan: " + ProdAvailableQuan.ToString());
            foreach (RepeaterItem item in rptrProductDetails.Items)
            {
                var lblError = item.FindControl("lblError") as Label;
                lblError.Text = "";

                var txtnewcartquan = item.FindControl("chosenquan") as TextBox;
                if (txtnewcartquan.Text != "")
                {
                    NewCartQuan = Convert.ToInt32(txtnewcartquan.Text.ToString());

                    if (NewCartQuan > 0 && NewCartQuan <= ProdAvailableQuan)
                    {
                        double NewCartProdPrice = NewCartQuan * ProdSellPrice;
                        SqlCommand cmd1 = new SqlCommand("UPDATE Cart SET CartProdQuantity = " + NewCartQuan + ", CartProdPrice = " + NewCartProdPrice + " WHERE UserID = " + UserID + " AND ProductID = " + ProductID + "", con);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        ProdAvailableQuan -= NewCartQuan;
                        SqlCommand cmd3 = new SqlCommand("UPDATE Products SET ProdQuantity = " + ProdAvailableQuan + " WHERE ProductID = " + ProductID + "", con);
                        cmd3.ExecuteNonQuery();

                        System.Diagnostics.Debug.WriteLine("New Cart Quan" + NewCartQuan);
                        Response.Redirect("Cart");
                    }
                    else
                    {
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
                else
                {
                    lblError.Text = "Please Enter A Valid Quantity";
                }
            }
            
            

        }
    }
}