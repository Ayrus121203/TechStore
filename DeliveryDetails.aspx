<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="DeliveryDetails.aspx.cs" Inherits="TechStore.BillingDetails" %>
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

                <p class="text-center h1 fw-bold mb-5 mx-1 mx-md-4 mt-4">Delivery Information</p>

                <div class="mx-1 mx-md-4">

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fas fa-home fa-lg me-3 fa-fw"></i>
                    <div class="form-outline flex-fill mb-0">
                        <asp:TextBox ID="txtBillingAddress" Placeholder="Billing Address (Use Maps)" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                  </div>

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fas fa-user-md fa-lg me-3 fa-fw "></i>
                    <div class="form-outline flex-fill mb-0">
                        <asp:TextBox ID="txtName" Placeholder="Your Name" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                  </div>

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fas fa-phone fa-lg me-3 fa-fw"></i>
                    <div class="form-outline flex-fill mb-0">
                        <asp:TextBox ID="txtPhoneNum" Placeholder="Your Contact Number" TextMode="Phone" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                  </div>

                       <div class="d-flex justify-content-center mx-2 mb-2 mb-lg-2">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                           </div>
                  <div class="d-flex justify-content-center mx-4 mb-3 mb-lg-4">
                      <asp:Button ID="btnContinue" CssClass="btn btn-primary btn-lg" runat="server" Text="Continue" OnClick="btnContinue_Click" />
                  
                  </div>
                    
                </div>

              </div>
              <div class="col-md-10 col-lg-6 col-xl-7 d-flex align-items-center order-1 order-lg-2">
                  <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3988.6611865763484!2d103.84676001467031!3d1.379949998993567!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31da16eb64b0249d%3A0xe5f10ff680eed942!2sNanyang%20Polytechnic!5e0!3m2!1sen!2ssg!4v1673630046454!5m2!1sen!2ssg" width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</section>
</asp:Content>
