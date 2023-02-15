using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Security.Cryptography;
using System.Net.Http.Headers;
using System.Security;
using System.Security.Permissions;
using System.Diagnostics.Contracts;

namespace TechStore
{
    public partial class AdminEditProduct1 : System.Web.UI.Page
    {
        String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidatePriceRegex();
            if (!IsPostBack)
            {
                BindProductImages();
                DisplayProductDetails();
                BindProductBrand();
                BindProductCat();

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
                    foreach (RepeaterItem item in rptrProductDetails.Items)
                    {
                        var ddlBrand = item.FindControl("ddlBrand") as DropDownList;
                        ddlBrand.DataSource = dt;
                        ddlBrand.DataTextField = "BrandName";
                        ddlBrand.DataValueField = "BrandName";
                        ddlBrand.DataBind();
                        ddlBrand.Items.Insert(0, new ListItem("-Select-", "0"));
                    }
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
                    foreach (RepeaterItem item in rptrProductDetails.Items)
                    {
                        var ddlCat = item.FindControl("ddlCat") as DropDownList;
                        ddlCat.DataSource = dt;
                        ddlCat.DataTextField = "CategoryName";
                        ddlCat.DataValueField = "CategoryName";
                        ddlCat.DataBind();
                        ddlCat.Items.Insert(0, new ListItem("-Select-", "0"));
                    }
                }
            }
        }
        private void DisplayProductDetails()
        {
            Int32 ProductID = Convert.ToInt32(Request.QueryString["ProductID"]);
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE ProductID=" + ProductID + "", con);
            DataTable dtproddetails = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dtproddetails);
            
            string prodname = "";
            string prodbrand = "";
            int prodquan = 0;
            string proddes = "";
            double produnitprice = 0;
            double produsualprice=0;
            double prodsellprice = 0;
            double proddiscount = 0;
            string prodCategory = "";
            rptrProductDetails.DataSource = dtproddetails;
            rptrProductDetails.DataBind();
            if (dtproddetails.Rows.Count != 0)
            {
                prodname = dtproddetails.Rows[0][1].ToString();
                prodbrand = dtproddetails.Rows[0][2].ToString();
                prodquan = Convert.ToInt32(dtproddetails.Rows[0][3].ToString());
                proddes = dtproddetails.Rows[0][4].ToString();
                produnitprice = Convert.ToDouble(dtproddetails.Rows[0][5].ToString());
                produsualprice = Convert.ToDouble(dtproddetails.Rows[0][6].ToString());
                prodsellprice = Convert.ToDouble(dtproddetails.Rows[0][7].ToString());
                proddiscount = Convert.ToDouble(dtproddetails.Rows[0][8].ToString());
                prodCategory = dtproddetails.Rows[0][9].ToString();
                foreach (RepeaterItem item in rptrProductDetails.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {

                        var txtprodname = item.FindControl("txtprodname") as TextBox;
                        var ddlBrand = item.FindControl("ddlBrand") as DropDownList;
                        var txtprodquan = item.FindControl("txtprodquan") as TextBox;
                        var txtproddes = item.FindControl("txtproddes") as TextBox;
                        var txtunitprice = item.FindControl("txtunitprice") as TextBox;
                        var txtusualprice = item.FindControl("txtusualprice") as TextBox;
                        var txtsellprice = item.FindControl("txtsellprice") as TextBox;
                        var txtproddis = item.FindControl("txtproddis") as TextBox;
                        var ddlCat = item.FindControl("ddlCat") as DropDownList;
                        txtprodname.Text = prodname;
                        ddlBrand.SelectedValue = prodbrand;
                        txtprodquan.Text = prodquan.ToString();
                        txtproddes.Text = proddes;
                        txtunitprice.Text = produnitprice.ToString();
                        txtusualprice.Text = produsualprice.ToString();
                        txtsellprice.Text = prodsellprice.ToString();
                        txtproddis.Text = proddiscount.ToString();
                        ddlCat.SelectedValue = prodCategory;
                    }
                }
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


        private bool ValidateForm()
        {
            foreach (RepeaterItem item in rptrProductDetails.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Int32 ProductID = Convert.ToInt32(Request.QueryString["ProductID"]);
                    var txtprodname = item.FindControl("txtprodname") as TextBox;
                    var ddlBrand = item.FindControl("ddlBrand") as DropDownList;
                    var ddlCat = item.FindControl("ddlCat") as DropDownList;
                    var txtprodquan = item.FindControl("txtprodquan") as TextBox;
                    var txtproddes = item.FindControl("txtproddes") as TextBox;
                    var txtunitprice = item.FindControl("txtunitprice") as TextBox;
                    var txtusualprice = item.FindControl("txtusualprice") as TextBox;
                    var txtsellprice = item.FindControl("txtsellprice") as TextBox;
                    var txtproddis = item.FindControl("txtproddis") as TextBox;
                    var fuimg01 = item.FindControl("fuImg01") as FileUpload;
                    var fuImg02 = item.FindControl("fuImg02") as FileUpload;
                    var fuimg03 = item.FindControl("fuimg03") as FileUpload;
                    var fuImg04 = item.FindControl("fuimg04") as FileUpload;
                    var lblMessage = item.FindControl("lblMessage") as Label;

                    if (txtprodname.Text != "" && ddlBrand.SelectedItem.Value != "0" && ddlCat.SelectedItem.Value!="0" && txtprodquan.Text != "" && txtproddes.Text != "" && txtunitprice.Text != "" && txtusualprice.Text != "" && txtsellprice.Text != "" && fuimg01.HasFile)
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
                                        SqlConnection con = new SqlConnection(CS);
                                        
                                        SqlCommand cmd = new SqlCommand("UPDATE Products SET ProdName=@ProdName, ProdBrand=@ProdBrand, ProdQuantity=@ProdQuantity, ProdDes=@ProdDes,ProdUnitPrice=@ProdUnitPrice,ProdUsualPrice=@ProdUsualPrice, ProdSellPrice=@ProdSellPrice, ProdDiscount=@ProdDiscount, ProdCategory=@ProdCategory WHERE ProductID=@ProdID", con);
                                        con.Open();
                                        cmd.Parameters.AddWithValue("@ProdName", txtprodname.Text);
                                        cmd.Parameters.AddWithValue("@ProdBrand", ddlBrand.SelectedItem.Value);
                                        cmd.Parameters.AddWithValue("@ProdQuantity", Convert.ToInt32(txtprodquan.Text));
                                        cmd.Parameters.AddWithValue("@ProdDes", txtproddes.Text);
                                        cmd.Parameters.AddWithValue("@ProdUnitPrice", Convert.ToDouble(txtunitprice.Text));
                                        cmd.Parameters.AddWithValue("@ProdUsualPrice", Convert.ToDouble(txtusualprice.Text));
                                        cmd.Parameters.AddWithValue("@ProdSellPrice", Convert.ToDouble(txtsellprice.Text));
                                        cmd.Parameters.AddWithValue("@ProdDiscount", Convert.ToDouble(txtproddis.Text));
                                        cmd.Parameters.AddWithValue("@ProdCategory", ddlCat.SelectedItem.Value);
                                        cmd.Parameters.AddWithValue("@ProdID", ProductID);
                                        
                                        cmd.ExecuteNonQuery();

                                        SqlCommand cmd2 = new SqlCommand("DELETE FROM ProductImages WHERE ProductID=" + ProductID + "", con);
                                        cmd2.ExecuteNonQuery();

                                        SqlCommand cmd3 = new SqlCommand("SELECT Name, Extension FROM ProductImages WHERE ProductID=" + ProductID + "", con);
                                        SqlDataAdapter sdaOldPics = new SqlDataAdapter(cmd3);
                                        DataTable dtOldPics = new DataTable();
                                        sdaOldPics.Fill(dtOldPics);
                                        string oldpic1;
                                        string oldpic2;
                                        string oldpic3;
                                        string oldpic4;
                                        if (dtOldPics.Rows.Count != 0)
                                        {
                                            string oldpic1Name = dtOldPics.Rows[0][0].ToString();
                                            string oldpic1Extension = dtOldPics.Rows[0][1].ToString();
                                            string oldpic2Name = dtOldPics.Rows[1][0].ToString();
                                            string oldpic2Extension = dtOldPics.Rows[1][1].ToString();
                                            string oldpic3Name = dtOldPics.Rows[2][0].ToString();
                                            string oldpic3Extension = dtOldPics.Rows[2][1].ToString();
                                            string oldpic4Name = dtOldPics.Rows[3][0].ToString();
                                            string oldpic4Extension = dtOldPics.Rows[3][1].ToString();
                                            string oldPath = Server.MapPath("~/Images/ProductImages/") + ProductID;
                                            oldpic1 = oldPath + "/" + oldpic1Name.ToString().Trim() + "01" + oldpic1Extension;
                                            oldpic2 = oldPath + "/" + oldpic2Name.ToString().Trim() + "02" + oldpic2Extension;
                                            oldpic3 = oldPath + "/" + oldpic3Name.ToString().Trim() + "03" + oldpic3Extension;
                                            oldpic4 = oldPath + "/" + oldpic4Name.ToString().Trim() + "04" + oldpic4Extension;
                                            File.Delete(oldpic1);
                                            File.Delete(oldpic2);
                                            File.Delete(oldpic3);
                                            File.Delete(oldpic4);
                                        }

                                        string SavePath = Server.MapPath("~/Images/ProductImages/") + ProductID;
                                        if (!Directory.Exists(SavePath))
                                        {
                                            Directory.CreateDirectory(SavePath);
                                        }
                                        string Extention01 = Path.GetExtension(fuimg01.PostedFile.FileName);
                                        string Extention02 = Path.GetExtension(fuImg02.PostedFile.FileName);
                                        string Extention03= Path.GetExtension(fuimg03.PostedFile.FileName);
                                        string Extention04 = Path.GetExtension(fuImg04.PostedFile.FileName);
                                        fuimg01.SaveAs(SavePath + "\\" + txtprodname.Text.ToString().Trim() + "01" + Extention01);
                                        fuImg02.SaveAs(SavePath + "\\" + txtprodname.Text.ToString().Trim() + "02" + Extention02);
                                        fuimg03.SaveAs(SavePath + "\\" + txtprodname.Text.ToString().Trim() + "03" + Extention03);
                                        fuImg04.SaveAs(SavePath + "\\" + txtprodname.Text.ToString().Trim() + "04" + Extention04);
                                        SqlCommand cmd4 = new SqlCommand("insert into ProductImages values(@ProdID, @ImageName, @Extension)", con);
                                        cmd4.Parameters.AddWithValue("@ProdID", ProductID);
                                        cmd4.Parameters.AddWithValue("@ImageName", txtprodname.Text.ToString().Trim() + "01");
                                        cmd4.Parameters.AddWithValue("@Extension",Extention01);
                                        cmd4.ExecuteNonQuery();
                                        SqlCommand cmd5 = new SqlCommand("insert into ProductImages values(@ProdID, @ImageName, @Extension)", con);
                                        cmd5.Parameters.AddWithValue("@ProdID", ProductID);
                                        cmd5.Parameters.AddWithValue("@ImageName", txtprodname.Text.ToString().Trim() + "02");
                                        cmd5.Parameters.AddWithValue("@Extension", Extention02);
                                        cmd5.ExecuteNonQuery();
                                        SqlCommand cmd6 = new SqlCommand("insert into ProductImages values(@ProdID, @ImageName, @Extension)", con);
                                        cmd6.Parameters.AddWithValue("@ProdID", ProductID);
                                        cmd6.Parameters.AddWithValue("@ImageName", txtprodname.Text.ToString().Trim() + "03");
                                        cmd6.Parameters.AddWithValue("@Extension", Extention03);
                                        cmd6.ExecuteNonQuery();
                                        SqlCommand cmd7 = new SqlCommand("insert into ProductImages values(@ProdID, @ImageName, @Extension)", con);
                                        cmd7.Parameters.AddWithValue("@ProdID", ProductID);
                                        cmd7.Parameters.AddWithValue("@ImageName", txtprodname.Text.ToString().Trim() + "04");
                                        cmd7.Parameters.AddWithValue("@Extension", Extention04);
                                        cmd7.ExecuteNonQuery();
                                        return true;
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
                else
                {
                    return false;
                }
            }
            return false;
        }

        private bool ValidateProdNameUnique()
        {
            Int32 ProductID = Convert.ToInt32(Request.QueryString["ProductID"].ToString());
            foreach (RepeaterItem item in rptrProductDetails.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var txtprodname = item.FindControl("txtprodname") as TextBox;
                    var lblMessage = item.FindControl("lblMessage") as Label;
                    SqlConnection con = new SqlConnection(CS);
                    SqlCommand cmd = new SqlCommand("SELECT ProdName FROM Products WHERE ProdName='" + txtprodname.Text + "' AND ProductID != "+ProductID+"", con);
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
                else
                {
                    return false;
                }
            }
            return false;
        }

        private bool ValidatePriceRegex()
        {
            string priceregex = "^\\$?(([1-9](\\d*|\\d{0,2}(,\\d{3})*))|0)(\\.\\d{1,2})?$";
            Regex pricevalidate = new Regex(priceregex);
            foreach (RepeaterItem item in rptrProductDetails.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var txtunitprice = item.FindControl("txtunitprice") as TextBox;
                    var txtusualprice = item.FindControl("txtusualprice") as TextBox;
                    var txtsellprice = item.FindControl("txtsellprice") as TextBox;
                    var txtproddis = item.FindControl("txtproddis") as TextBox;
                    var lblMessage = item.FindControl("lblMessage") as Label;
                    string strtxtunitprice = txtunitprice.Text.ToString();
                    string strtxtusualprice = txtusualprice.Text.ToString();
                    string strtxtsellprice = txtsellprice.Text.ToString();
                    if (pricevalidate.IsMatch(strtxtunitprice) && pricevalidate.IsMatch(strtxtusualprice) && pricevalidate.IsMatch(strtxtsellprice))
                    {
                        txtproddis.Text = (Convert.ToDouble(txtusualprice.Text) - Convert.ToDouble(txtsellprice.Text)).ToString();
                        return true;
                    }
                    else
                    {
                        lblMessage.Text = "Selling Price Must Be Equal To Or Less Than Usual Price";
                        return false;
                    }  
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private bool ValidateProductDiscountAmt()
        {
            foreach (RepeaterItem item in rptrProductDetails.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var txtusualprice = item.FindControl("txtusualprice") as TextBox;
                    var txtsellprice = item.FindControl("txtsellprice") as TextBox;
                    var lblMessage = item.FindControl("lblMessage") as Label;
                    if (Convert.ToDouble(txtusualprice.Text) >= Convert.ToDouble(txtsellprice.Text)) //Validate Discount
                    {
                        lblMessage.Text = "";
                        return true;
                    }
                    else
                    {
                        lblMessage.Text = "Selling Price Must Be Equal To Or Less Than Usual Price";
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private bool ValidateSellingPrice()
        {
            foreach (RepeaterItem item in rptrProductDetails.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var txtunitprice = item.FindControl("txtunitprice") as TextBox;
                    var txtusualprice = item.FindControl("txtusualprice") as TextBox;
                    var lblMessage = item.FindControl("lblMessage") as Label;
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
                return false;
            }
            return false;
        }
        protected void btnupdateprod_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                Response.Redirect("AdminHome");
            }
        }
    }
}