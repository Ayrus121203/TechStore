using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using System.Data;

namespace TechStore
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RegisterRoutes(RouteTable.Routes);

        }
        static void RegisterRoutes(RouteCollection routes)
        {
            //routes.MapPageRoute("Uniquename", "Name to shown on Adddress bar AND for redirecting", "Physical Path to the page");
            routes.MapPageRoute("UAddProduct", "AddProducts", "~/AddProduct.aspx");
            routes.MapPageRoute("UAddProductBrand", "AddProductBrand", "~/AddProductbrand.aspx");
            routes.MapPageRoute("UAddProductCat", "AddProductCategory", "~/AddProductCat.aspx");
            routes.MapPageRoute("UAdminDefault", "AdminHome", "~/AdminDefault.aspx");
            routes.MapPageRoute("UAdminEditProduct", "EditProduct", "~/AdminEditProduct.aspx");
            routes.MapPageRoute("UAdminRemoveProduct", "DeleteProduct", "~/AdminRemoveProduct.aspx");
            routes.MapPageRoute("UAdminOrders", "Orders", "~/AdminOrders.aspx");

            routes.MapPageRoute("UAdminViewProduct", "ViewProducts", "~/AdminViewProduct.aspx");
            routes.MapPageRoute("UCart", "Cart", "~/Cart.aspx");
            routes.MapPageRoute("UCheckoutCart", "Checkout", "~/CheckoutCart.aspx");
            routes.MapPageRoute("UDefault", "Home", "~/Default.aspx");
            routes.MapPageRoute("UDeliveryDetails", "DeliveryDetails", "~/DeliveryDetails.aspx");
            routes.MapPageRoute("UEditCartProd", "EditCartItem", "~/EditCartProd.aspx");

            routes.MapPageRoute("UFailure", "Error", "~/Failure.aspx");
            routes.MapPageRoute("ULogin", "Login", "~/Login.aspx");
            routes.MapPageRoute("UPaymentSuccess", "PaymentSuccess", "~/PaymentSuccess.aspx");
            routes.MapPageRoute("UProducts", "Products", "~/Products.aspx");
            routes.MapPageRoute("UProductView", "ProductView", "~/ProductView.aspx");
            routes.MapPageRoute("URegister", "SignUp", "~/Register.aspx");

            routes.MapPageRoute("URegisterUserSMS", "SMSVerify", "~/RegisterUserSMS.aspx");
            routes.MapPageRoute("USearchResult", "Search", "~/SearchResult.aspx");
            routes.MapPageRoute("UUnlockUserAccount_SMS", "SMSRecoverAcccount", "~/UnlockUserAccount_SMS.aspx");
            routes.MapPageRoute("UUserOrderHistory", "OrderHistory", "~/UserOrderHistory.aspx");
            routes.MapPageRoute("UUserProfile", "Profile", "~/UserProfile.aspx");
            routes.MapPageRoute("UForgotPasswordVerifyEmail", "ForgotPasswordEmailVerify", "~/ForgotPasswordVerifyEmail.aspx");
            routes.MapPageRoute("UAddSecurityQuestion", "EnableSecurityQuestions", "~/AddSecurityQuestion.aspx");
            
            routes.MapPageRoute("UResetPassword", "ResetPassword", "~/ResetPassword.aspx");
            routes.MapPageRoute("UForgotPass_AnswerSecQuestion", "ForgotPasswordSecurityQuestionVerification", "~/ForgotPass_AnswerSecQuestion.aspx");
            routes.MapPageRoute("UAccountRecovery_AnswerSecQuestion", "AccountRecoverySecurityQuestionVerification", "~/AccountRecovery_AnswerSecQuestion.aspx");
            routes.MapPageRoute("UAccountRecoveryPasswordReset", "AccountRecoveryPasswordReset", "~/AccountRecoveryPasswordReset.aspx");
            routes.MapPageRoute("ULogin2FASMS", "Login_2FA", "~/Login_SMS2FA.aspx");
            routes.MapPageRoute("UAdminProfile", "AdminProfile", "~/AdminProfile.aspx");
            routes.MapPageRoute("UUserOrderDetails", "UserOrderDetail", "~/UserOrderDetail.aspx");
            routes.MapPageRoute("UAdminOrderDetails", "AdminOrderDetails", "~/AdminOrderDetails.aspx");
            routes.MapPageRoute("UEnableDeviceProxAuth", "EnableDeviceProximityAuth", "~/EnableDeviceProxAuth.aspx");
            routes.MapPageRoute("ULoginDeviceProxAuth", "LoginDeviceProxAuth", "~/LoginDeviceProxAuth.aspx");
        }

        /// <summary>
        /// MapPageRoute(“unique name”, “display name”, “physical path”) do? When you do a Response.Redirect(), Home.aspx (physical path)
        /// </summary>
        
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}