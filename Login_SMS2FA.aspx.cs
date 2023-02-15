using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Net.PeerToPeer;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace TechStore
{
    public partial class Login_SMS2FA : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string AccountSID = "ACd8092879c3f9fae29614d31c01da62ad";
        string Auth_Token = "4b0883e784eed280cc6fa455ce9f1e33";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LoginID"]!=null)
                {
                    SendOTPTwilio();
                }
                else
                {
                    Response.Redirect("Login");
                }
            }
        }

        protected void btn2FALogin_Click(object sender, EventArgs e)
        {
            VerifyOTP();
        }    
        public Int32 GetAccountID()
        {
            Int32 AccountID;
            if (Session["LoginID"] != null)
            {
                AccountID = Convert.ToInt32(Session["LoginID"]);
                return AccountID;
            }
            return -1;

        }
        public string GenerateOTP()
        {
            string otp;
            Random rand = new Random();
            otp = (rand.Next(1000, 9999)).ToString();
            System.Diagnostics.Debug.WriteLine(otp);
            return otp;
        }
        public string GetName()
        {
            string Name;
            Int32 AccountID;
            if (GetAccountID() != -1)
            {
                AccountID = GetAccountID();
                SqlConnection con = new SqlConnection(CS);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Name FROM Users WHERE UserID=@AccountID", con);
                cmd.Parameters.AddWithValue("@AccountID", AccountID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Name = dt.Rows[0][0].ToString();
                    return Name;
                }
            }
            return "";

        }
        public string GetPhoneNumbr()
        {
            string PhoneNumber;
            Int32 AccountID;
            if (GetAccountID() != -1)
            {
                AccountID = GetAccountID();
                SqlConnection con = new SqlConnection(CS);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT PhoneNumber FROM Users WHERE UserID=@AccountID", con);
                cmd.Parameters.AddWithValue("@AccountID", AccountID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    PhoneNumber = dt.Rows[0][0].ToString();
                    return PhoneNumber;
                }
            }
            return "";
        }

        public void SendOTPTwilio()
        {
            string otp = GenerateOTP();
            string telphone;
            string name;
            if (GetName() != "")
            {
                name = GetName();
                if (GetPhoneNumbr() != "")
                {
                    telphone = "+65" + GetPhoneNumbr();
                    phonenum.InnerText = telphone;
                    string otpMessage = "Dear " + name + ". Your OTP is " + otp + ".";
                    //Use service provider to establish connection
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    try {
                        TwilioClient.Init(AccountSID, Auth_Token);
                        var message = MessageResource.Create(
                            body: otpMessage,
                            from: new Twilio.Types.PhoneNumber("+17262247257"),
                            to: new Twilio.Types.PhoneNumber(telphone)
                              );
                        Console.Write(message.Sid);
                        Session["OTP"] = otp;
                        System.Diagnostics.Debug.WriteLine(Session["OTP"].ToString());
                    }
                    catch
                    {
                        Response.Redirect("Login");
                    }
                }
            }
        }
        private string GetUserType()
        {
            string UserType;
            Int32 AccountID = GetAccountID();

            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT UserType FROM Users WHERE UserID=@AccountID", con);
            cmd.Parameters.AddWithValue("@AccountID", AccountID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                UserType = dt.Rows[0][0].ToString();
                return UserType;
            }
            else
            {
                return "";
            }
        }
        private void VerifyOTP()
        {
            if (txtOTP.Text == Session["OTP"].ToString())
            {
                Int32 AccountID = Convert.ToInt32(Session["LoginID"].ToString());
                Session["OTP"] = null;
                string Usertype;
                if (GetUserType() != "")
                {
                    Usertype = GetUserType();
                    if (Usertype == "A")
                    {
                        Session["AdminID"]=GetAccountID();
                        Response.Redirect("AdminProfile");
                    }
                    else if(Usertype == "U")
                    {
                        Session["UserID"] = GetAccountID();
                        Response.Redirect("Profile");
                    }
                }
            }
            else if (txtOTP.Text != Session["OTP"].ToString())
            {
                Session["LoginID"] = null;
                Session["OTP"] = null;
                Response.Redirect("Home");
            }
            else
            {
                Session["LoginID"] = null;
                Session["OTP"] = null;
                Response.Redirect("SignUp");
            }
        }
    }
}