using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Twilio;
using Twilio.Clients;
using System.Xml.Linq;
using Twilio.Rest.Api.V2010.Account;
using System.Net;
using System.Net.PeerToPeer;

namespace TechStore

{
    public partial class RegisterUserEmailCallback : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string AccountSID = "ACd8092879c3f9fae29614d31c01da62ad";
        string Auth_Token = "4b0883e784eed280cc6fa455ce9f1e33";




        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["TelNo"] != null && Session["Name"] != null)
                {
                    SendOTPTwilio();
                }
                else
                {
                    Response.Redirect("SignUp");
                }
            }
        }

        public string GenerateOTP()
        {
            string otp;
            Random rand = new Random();
            otp= (rand.Next(1000,9999)).ToString();
            System.Diagnostics.Debug.WriteLine(otp);
            return otp;
        }

        public void SendOTPTwilio()
        {
            string otp = GenerateOTP();
            string telphone = "+65" + Session["TelNo"].ToString();
            phonenum.InnerText=telphone;
            string name = Session["Name"].ToString();
            string otpMessage = "Dear " + name + ". Your OTP is " + otp + ".";
            //Use service provider to establish connection
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            TwilioClient.Init(AccountSID, Auth_Token);
            var message = MessageResource.Create(
                body: otpMessage,
                from: new Twilio.Types.PhoneNumber("+17262247257"),
                to: new Twilio.Types.PhoneNumber(telphone)
                  ) ;
            Console.Write(message.Sid);
            Session["OTP"] = otp;
            System.Diagnostics.Debug.WriteLine(Session["OTP"].ToString());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtOTP.Text == Session["OTP"].ToString())
            {
                Session["OTP"] = null;
                string Username= Session["Username"].ToString();
                string Name= Session["Name"].ToString();
                string hashedpw= Session["Password"].ToString();
                string Email=Session["Email"].ToString();
                string TelNo= Session["TelNo"].ToString();
                SqlConnection con = new SqlConnection(CS);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, Password, Email, Name, PhoneNumber, UserType, LoginAttemptCount, AccountStatus) VALUES(@Username, @Password, @Email, @Name, @PhoneNumber, @UserType, @LoginAttemptCount, @AccountStatus)", con);
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@Password", hashedpw);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@PhoneNumber", TelNo);
                cmd.Parameters.AddWithValue("@UserType", "U");
                cmd.Parameters.AddWithValue("@LoginAttemptCount", 0);
                cmd.Parameters.AddWithValue("@AccountStatus", "UNLOCKED");
                cmd.ExecuteNonQuery();
                Response.Redirect("Login");
            }
            else if(txtOTP.Text != Session["OTP"].ToString())
            {
                Session["OTP"] = null;
                Response.Redirect("Home");
            }
            else
            {
                Session["OTP"] = null;
                Response.Redirect("SignUp");
            }
        }
    }
}