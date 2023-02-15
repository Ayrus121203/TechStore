<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="LoginDeviceProxAuth.aspx.cs" Inherits="TechStore.LoginDeviceProxAuth" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="mainDiv">
  <div class="cardStyle">
      
      <img src="" id="signupLogo"/>
      
      <h2 class="formTitle">
        Login via bluetooth authentication
      </h2>
      
    <div class="inputDiv">
        <asp:Label ID="Label1" CssClass="inputLabel" runat="server" Text="Enter Your Username"></asp:Label>
        <asp:TextBox ID="txtUsername" CssClass="input-resetpass" runat="server"></asp:TextBox>
    </div>
    <div class="inputDiv">
        <asp:Label ID="Label2" CssClass="inputLabel" runat="server" Text="Device Address"></asp:Label>
        <asp:TextBox ID="txtDeviceAddress" MaxLength="50" CssClass="input-resetpass" ReadOnly="true" TextMode="Password" runat="server"></asp:TextBox>
    </div>
    
    <div class="buttonWrapper">
        <asp:Button ID="btnCheckDeviceAddress" runat="server" Text="Search" OnClick="btnCheckDeviceAddress_Click" CssClass="submitButton pure-button pure-button-primary" />
        <asp:Label ID="lblMsg" runat="server" Text="" CssClass="text-danger" Style="display:inline-flex"></asp:Label>
    </div>
  </div>
</div>
</asp:Content>
