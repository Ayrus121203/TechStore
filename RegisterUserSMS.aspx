<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="RegisterUserSMS.aspx.cs" Inherits="TechStore.RegisterUserEmailCallback" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-center align-items-center container-sms">
        <div class="card-sms py-5 px-3">
            <h5 class="m-0">Register Your Account</h5><span class="mobile-text-sms">Enter the code we just send on your mobile phone <b class="text-danger" runat="server" id="phonenum"></b></span>
            <div class="d-flex flex-row mt-5">
                <asp:TextBox ID="txtOTP" CssClass="form-control" runat="server"></asp:TextBox>
                <div style="padding-left:10px;">
                <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Verify" OnClick="Button1_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
