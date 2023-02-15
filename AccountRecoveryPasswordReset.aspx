<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="AccountRecoveryPasswordReset.aspx.cs" Inherits="TechStore.AccountRecoveryPasswordReset" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="mainDiv">
  <div class="cardStyle">
      
      <img src="Images/TechStoreLogo.png" id="signupLogo" style="width:70%; height:80%"/>
      
      <h2 class="formTitle">
        Recover your Account
      </h2>
      <h5 class="formTitle">
          Reset your password before unlocking your password. 
      </h5>
    <div class="inputDiv">
        <asp:Label ID="Label1" CssClass="inputLabel" runat="server" Text="New Password"></asp:Label>
        <asp:TextBox ID="txtNewPass" TextMode="Password" CssClass="input-resetpass" runat="server"></asp:TextBox>
        <h6 class="text-muted">Please ensure you use a different password</h6>
    </div>
    <div class="inputDiv">
        <asp:Label ID="Label2" CssClass="inputLabel" runat="server" Text="Confirm Password"></asp:Label>
        <asp:TextBox ID="txtConPass" TextMode="Password" CssClass="input-resetpass" runat="server"></asp:TextBox>
    </div>
    
    <div class="buttonWrapper">
        <asp:Button ID="btnResetPas" runat="server" Text="Reset Password" OnClick="btnResetPas_Click" CssClass="submitButton pure-button pure-button-primary" />
        <asp:Label ID="lblMsg" runat="server" Text="" Style="display:inline-flex; text-align:center"></asp:Label>
    </div>
     
  </div>
</div>
<%-- Modal --%>
                    
</asp:Content>
