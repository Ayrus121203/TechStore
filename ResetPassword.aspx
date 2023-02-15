<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="TechStore.ResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="mainDiv">
  <div class="cardStyle">
      
      <img src="" id="signupLogo"/>
      
      <h2 class="formTitle">
        Reset Your Password
      </h2>
      
    <div class="inputDiv">
        <asp:Label ID="Label1" CssClass="inputLabel" runat="server" Text="New Password"></asp:Label>
        <asp:TextBox ID="txtNewPass" TextMode="Password" CssClass="input-resetpass" runat="server"></asp:TextBox>
    </div>
    <div class="inputDiv">
        <asp:Label ID="Label2" CssClass="inputLabel" runat="server" Text="Confirm Password"></asp:Label>
        <asp:TextBox ID="txtConPass" TextMode="Password" CssClass="input-resetpass" runat="server"></asp:TextBox>
    </div>
    
    <div class="buttonWrapper">
        <asp:Button ID="btnResetPas" runat="server" Text="Reset Password" OnClick="btnResetPas_Click" CssClass="submitButton pure-button pure-button-primary" />
        <asp:Label ID="lblMsg" runat="server" Text="" Style="display:inline-flex"></asp:Label>
    </div>
     
  </div>
</div>
</asp:Content>
