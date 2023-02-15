<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="EnableDeviceProxAuth.aspx.cs" Inherits="TechStore.EnableDeviceProxAuth" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="mainDiv">
  <div class="cardStyle">
      
      <img src="" id="signupLogo"/>
      
      <h2 class="formTitle">
        Add Bluetooth Auth Device
      </h2>
      
    <div class="inputDiv">
        <asp:Label ID="Label1" CssClass="inputLabel" runat="server" Text="Device Name"></asp:Label>
        <asp:TextBox ID="txtDeviceName" CssClass="input-resetpass" ReadOnly="true" runat="server"></asp:TextBox>
    </div>
    <div class="inputDiv">
        <asp:Label ID="Label2" CssClass="inputLabel" runat="server" Text="Device Address"></asp:Label>
        <asp:TextBox ID="txtDeviceAddress" MaxLength="50" CssClass="input-resetpass" ReadOnly="true" TextMode="Password" runat="server"></asp:TextBox>
    </div>
    
    <div class="buttonWrapper">
        <asp:Button ID="btnAdd" runat="server" Text="Verify" OnClick="btnAdd_Click" CssClass="submitButton pure-button pure-button-primary" />
        <div style="padding-top:10px">
        <asp:Button ID="Button1" runat="server" Text="Refresh" CssClass="submitButton pure-button pure-button-primary" />
        </div>
        <asp:Label ID="lblMsg" runat="server" Text="" CssClass="text-danger" Style="display:inline-flex"></asp:Label>
    </div>
     
  </div>
</div>
</asp:Content>
