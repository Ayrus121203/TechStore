<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="AddSecurityQuestion.aspx.cs" Inherits="TechStore.AddSecurityQuestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
    $("#btnAdd").click(function() { 
   // For example, in the click event of your button
   event.preventDefault(); // Stop the default action, which is to post
    });
   </script>
<div class="mainDiv">
  <div class="cardStyle">
      
      <img src="" id="signupLogo"/>
      
      <h2 class="formTitle">
        Add Security Question
      </h2>
      
    <div class="inputDiv">
        <asp:Label ID="Label1" CssClass="inputLabel" runat="server" Text="Security Question"></asp:Label>
        <asp:DropDownList ID="ddlQuestions" runat="server" CssClass="input-resetpass"></asp:DropDownList>
    </div>
    <div class="inputDiv">
        <asp:Label ID="Label2" CssClass="inputLabel" runat="server" Text="Answer"></asp:Label>
        <asp:TextBox ID="txtAns" MaxLength="50" CssClass="input-resetpass" runat="server"></asp:TextBox>
    </div>
    
    <div class="buttonWrapper">
        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="submitButton pure-button pure-button-primary" />
        <asp:Label ID="lblMsg" runat="server" Text="" Style="display:inline-flex"></asp:Label>
    </div>
     
  </div>
</div>

<section id="displaymodal" runat="server">
    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Answer already exists</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
              <img src="Images/updatesecques.jpg" width="90" height="90" style="margin-right:auto;margin-left:auto;display:block;" />
              <br />
            You already have a answer for the selected question. <br />
            If u wish to update your answer, click 'Update' else, click 'Close
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            <asp:Button ID="btnUpdateAns" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnUpdateAns_Click" />
          </div>
        </div>
      </div>
    </div>
</section>

</asp:Content>
