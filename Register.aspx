<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TechStore.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<section class="vh-100">
  <div class="container h-100">
    <div class="row d-flex justify-content-center align-items-center h-100">
      <div class="col-lg-12 col-xl-11">
        <div class="card text-black" style="border:none;">
          <div class="card-body p-md-5">
            <div class="row justify-content-center">
              <div class="col-md-10 col-lg-6 col-xl-5 order-2 order-lg-1">

                <p class="text-center h1 fw-bold mb-5 mx-1 mx-md-4 mt-4">Sign up</p>

                <div class="mx-1 mx-md-4">

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fas fa-user fa-lg me-3 fa-fw"></i>
                    <div class="form-outline flex-fill mb-0">
                        <asp:TextBox ID="txtUserName" Placeholder="Your UserName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                  </div>

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fas fa-user-md fa-lg me-3 fa-fw "></i>
                    <div class="form-outline flex-fill mb-0">
                        <asp:TextBox ID="txtName" Placeholder="Your Name" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                  </div>

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fas fa-envelope fa-lg me-3 fa-fw"></i>
                    <div class="form-outline flex-fill mb-0">
                        <asp:TextBox ID="txtEmail" Placeholder="Your Email" TextMode="Email" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                  </div>

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fas fa-phone fa-lg me-3 fa-fw"></i>
                    <div class="form-outline flex-fill mb-0">
                        <asp:TextBox ID="txttelnum" Placeholder="Your Phone Number" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                  </div>

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fas fa-lock fa-lg me-3 fa-fw"></i>
                    <div class="form-outline flex-fill mb-0">
                        <asp:TextBox ID="txtPass" Placeholder="Your Password" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                  </div>

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fas fa-key fa-lg me-3 fa-fw"></i>
                    <div class="form-outline flex-fill mb-0">
                        <asp:TextBox ID="txtConPass" Placeholder="Confirm Password" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                  </div>

            <div class="form-check d-flex justify-content-start mb-4">
                <span class="form-check-label"><asp:Label ID="lblRemPassChkbx" runat="server" Text="I agree all statements in "></asp:Label><a href="#"><asp:Label ID="Label1" runat="server" Text="Terms of service"></asp:Label></a></span>
                <asp:CheckBox ID="chkbxRemPass" CssClass="form-check-input" runat="server" />
            </div>
                       <div class="d-flex justify-content-center mx-2 mb-2 mb-lg-2">
                    <asp:Label ID="lblError" runat="server" CssClass="text-danger text-warning"></asp:Label>
                           </div>
                  <div class="d-flex justify-content-center mx-4 mb-3 mb-lg-4">
                      <asp:Button ID="btnRegister" CssClass="btn btn-primary btn-lg" runat="server" Text="Register" OnClick="btnRegister_Click" />
                  </div>
                </div>

              </div>
              <div class="col-md-10 col-lg-6 col-xl-7 d-flex align-items-center order-1 order-lg-2">

                <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-registration/draw1.webp"
                  class="img-fluid" alt="Sample image">

              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</section>
</asp:Content>
