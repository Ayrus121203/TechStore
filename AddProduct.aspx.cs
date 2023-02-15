using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace TechStore
{
    public partial class AddProduct : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                BindProductBrand();
                BindProductCat();
            }
            else
            {
                ValidatePriceRegex();
            }
        }
        private void BindProductBrand()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM ProductBrands", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    ddlBrand.DataSource = dt;
                    ddlBrand.DataTextField = "BrandName";
                    ddlBrand.DataValueField = "BrandName";
                    ddlBrand.DataBind();
                    ddlBrand.Items.Insert(0, new ListItem("-Select-", "0"));
                }
            }
        }
        private void BindProductCat()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM ProductCategories", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    ddlCat.DataSource = dt;
                    ddlCat.DataTextField = "CategoryName";
                    ddlCat.DataValueField = "CategoryName";
                    ddlCat.DataBind();
                    ddlCat.Items.Insert(0, new ListItem("-Select-", "0"));
                }
            }
        }
        private bool ValidateForm()
        {

            Int32 ProductID = Convert.ToInt32(Request.QueryString["ProductID"]);

            
            if (txtPName.Text != "" && ddlBrand.SelectedItem.Value != "0" && txtQuantity.Text != "" && txtDesc.Text != "" && txtunitprice.Text != "" && txtusualprice.Text != "" && txtSelPrice.Text != "" && ddlCat.SelectedItem.Value!="0" && fuImg01.HasFile)
            {
                if (ValidateProdNameUnique())
                {
                    if (ValidatePriceRegex())
                    {
                        if (ValidateSellingPrice())
                        {
                            if (ValidateProductDiscountAmt())
                            {
                                lblMessage.Text = "";
                                using (SqlConnection con = new SqlConnection(CS))
                                {
                                    
                                    SqlCommand cmd = new SqlCommand("procInsertProducts", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@PName", txtPName.Text);
                                    cmd.Parameters.AddWithValue("@PBrand", ddlBrand.SelectedItem.Value);
                                    cmd.Parameters.AddWithValue("@PQuantity", txtQuantity.Text);
                                    cmd.Parameters.AddWithValue("@PDes", txtDesc.Text);
                                    cmd.Parameters.AddWithValue("@PUnitPrice", txtunitprice.Text);
                                    cmd.Parameters.AddWithValue("@PUsualPrice", txtusualprice.Text);
                                    cmd.Parameters.AddWithValue("@PSellPrice", txtSelPrice.Text);
                                    cmd.Parameters.AddWithValue("@PDiscount", txtproddis.Text);
                                    cmd.Parameters.AddWithValue("@PCategory", ddlCat.SelectedItem.Value);

                                    con.Open();
                                    Int32 PID = Convert.ToInt32(cmd.ExecuteScalar());

                                    //Insert and upload Images
                                    if (fuImg01.HasFile)
                                    {
                                        string SavePath = Server.MapPath("~/Images/ProductImages/") + PID;
                                        if (!Directory.Exists(SavePath))
                                        {
                                            Directory.CreateDirectory(SavePath);
                                        }
                                        string Extention = Path.GetExtension(fuImg01.PostedFile.FileName);
                                        fuImg01.SaveAs(SavePath + "\\" + txtPName.Text.ToString().Trim() + "01" + Extention);

                                        SqlCommand cmd3 = new SqlCommand("insert into ProductImages values('" + PID + "','" + txtPName.Text.ToString().Trim() + "01" + "','" + Extention + "')", con);
                                        cmd3.ExecuteNonQuery();
                                    }
                                    if (fuImg02.HasFile)
                                    {
                                        string SavePath = Server.MapPath("~/Images/ProductImages/") + PID;
                                        if (!Directory.Exists(SavePath))
                                        {
                                            Directory.CreateDirectory(SavePath);
                                        }
                                        string Extention = Path.GetExtension(fuImg02.PostedFile.FileName);
                                        fuImg02.SaveAs(SavePath + "\\" + txtPName.Text.ToString().Trim() + "02" + Extention);

                                        SqlCommand cmd4 = new SqlCommand("insert into ProductImages values('" + PID + "','" + txtPName.Text.ToString().Trim() + "02" + "','" + Extention + "')", con);
                                        cmd4.ExecuteNonQuery();
                                    }
                                    if (fuImg03.HasFile)
                                    {
                                        string SavePath = Server.MapPath("~/Images/ProductImages/") + PID;
                                        if (!Directory.Exists(SavePath))
                                        {
                                            Directory.CreateDirectory(SavePath);
                                        }
                                        string Extention = Path.GetExtension(fuImg03.PostedFile.FileName);
                                        fuImg03.SaveAs(SavePath + "\\" + txtPName.Text.ToString().Trim() + "03" + Extention);

                                        SqlCommand cmd5 = new SqlCommand("insert into ProductImages values('" + PID + "','" + txtPName.Text.ToString().Trim() + "03" + "','" + Extention + "')", con);
                                        cmd5.ExecuteNonQuery();
                                    }
                                    if (fuImg04.HasFile)
                                    {
                                        string SavePath = Server.MapPath("~/Images/ProductImages/") + PID;
                                        if (!Directory.Exists(SavePath))
                                        {
                                            Directory.CreateDirectory(SavePath);
                                        }
                                        string Extention = Path.GetExtension(fuImg04.PostedFile.FileName);
                                        fuImg04.SaveAs(SavePath + "\\" + txtPName.Text.ToString().Trim() + "04" + Extention);

                                        SqlCommand cmd6 = new SqlCommand("insert into ProductImages values('" + PID + "','" + txtPName.Text.ToString().Trim() + "04" + "','" + Extention + "')", con);
                                        cmd6.ExecuteNonQuery();
                                    }
                                    return true;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                lblMessage.Text = "All Fields Are Mandatory";
                //Not all txt fields are filled
                return false;
            }

        }

        private bool ValidateProdNameUnique()
        {
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT ProdName FROM Products WHERE ProdName='" + txtPName.Text + "'", con);
            con.Open();
            DataTable dtexistingprodname = new DataTable();
            SqlDataAdapter sdaexistingprodname = new SqlDataAdapter(cmd);
            sdaexistingprodname.Fill(dtexistingprodname);
            if (dtexistingprodname.Rows.Count != 0) //Product Name Already Exists --> Return FALSE
            {
                lblMessage.Text = "Product Name Already Exists. Please Choose A New Name";
                return false;
            }
            else //Product Name is Unique --> Return TRUE
            {
                return true;
            }
        }

        private bool ValidatePriceRegex()
        {
            string priceregex = "^\\$?(([1-9](\\d*|\\d{0,2}(,\\d{3})*))|0)(\\.\\d{1,2})?$";
            Regex pricevalidate = new Regex(priceregex);
            string strtxtunitprice = txtunitprice.Text.ToString();
            string strtxtusualprice = txtusualprice.Text.ToString();
            string strtxtsellprice = txtSelPrice.Text.ToString();
            if (pricevalidate.IsMatch(strtxtunitprice) && pricevalidate.IsMatch(strtxtusualprice) && pricevalidate.IsMatch(strtxtsellprice))
            {
                lblMessage.Text = "";
                txtproddis.Text = (Convert.ToDouble(txtusualprice.Text) - Convert.ToDouble(txtSelPrice.Text)).ToString();
                return true;
            }
            else
            {
                lblMessage.Text = "Ensure Price Fields Are In The Right Format";   
                return false;
            }
        }

        private bool ValidateProductDiscountAmt()
        {

            if (Convert.ToDouble(txtusualprice.Text) >= Convert.ToDouble(txtSelPrice.Text)) //Validate Discount
            {
                lblMessage.Text = "";
                return true;
            }
            else
            {
                lblMessage.Text = "Selling Price Must Be Equal To Or Less Than Usual Price";
                return false;
            }
        }

        private bool ValidateSellingPrice()
        {
            if (Convert.ToDouble(txtusualprice.Text) >= Convert.ToDouble(txtunitprice.Text))
            {
                lblMessage.Text = "";
                return true;
            }
            else
            {
                lblMessage.Text = "Usual Price Must Be More Than Unit Price";
                return false;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                Response.Redirect("ViewProducts");
            }
        }
    }
}