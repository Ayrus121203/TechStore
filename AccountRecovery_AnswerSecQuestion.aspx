<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="AccountRecovery_AnswerSecQuestion.aspx.cs" Inherits="TechStore.AccountRecovery_AnswerSecQuestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="mainDiv">
  <div class="cardStyle">
      
      <img src="" id="signupLogo"/>
      
      <h2 class="formTitle">
        Recover Account
      </h2>      
    <div class="inputDiv">
        <asp:Label ID="Label1" CssClass="inputLabel" runat="server" Text="Security Question"></asp:Label>
        <asp:DropDownList ID="ddlQuestions" runat="server" CssClass="input-resetpass"></asp:DropDownList>
    </div>
    <div class="inputDiv">
        <asp:Label ID="Label2" CssClass="inputLabel" runat="server" Text="Answer"></asp:Label>
        <asp:TextBox ID="txtInputAns" MaxLength="50" CssClass="input-resetpass" runat="server"></asp:TextBox>
    </div>
    <div class="buttonWrapper">
        <asp:Button ID="btnVerify" runat="server" Text="Verify" OnClick="btnVerify_Click" CssClass="submitButton pure-button pure-button-primary" />
        <asp:Label ID="lblMsg" runat="server" Text="" Style="display:inline-flex;text-align:center"></asp:Label>
    </div>
     
  </div>
</div>
</asp:Content>
