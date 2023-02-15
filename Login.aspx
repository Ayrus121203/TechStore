<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TechStore.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $("#btnEmail2FA").click(function () {
            // For example, in the click event of your button
            event.preventDefault(); // Stop the default action, which is to post
        });
    </script>
<section class="vh-100">
  <div class="container py-5 h-100">
    <div class="row d-flex justify-content-center align-items-center h-100">
      <div class="col-12 col-md-8 col-lg-6 col-xl-5">
        <div class="card shadow-2-strong" style="border-radius: 1rem;">
          <div class="card-body p-5 text-center">

            <h3 class="mb-4">Sign in</h3>
              
              <asp:Label ID="lblMsg" runat="server" ></asp:Label>
                <br />
              <br />
            <div class="form-outline mb-4">
                <asp:Label ID="lbl_Username" CssClass="form-label" runat="server" Text="Username"></asp:Label>
                <asp:TextBox ID="txtUsername" CssClass="form-control form-control-lg" runat="server"></asp:TextBox>
            </div>

            <div class="form-outline mb-4">
                <asp:Label ID="lblPassword" CssClass="form-label" runat="server" Text="Password"></asp:Label>
                <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control form-control-lg" runat="server"></asp:TextBox>
            </div>

            <!-- Checkbox -->
            <div class="form-check d-flex justify-content-start mb-4">
                <asp:Label ID="lblRemMe" CssClass="form-check-label" runat="server" Text="Remember password"></asp:Label>
                <asp:CheckBox ID="chckbxRemMe" CssClass="form-check-input" runat="server" />
            
            </div>
              <h6><a href="ForgotPasswordVerifyEmail.aspx">Forgot Password</a></h6>
              <br />
              <asp:Button ID="btnEmail2FA" CssClass="btn btn-primary btn-lg btn-block" runat="server" Text="Login" OnClick="btnEmail2FA_Click" />
              <asp:Button ID="btnPasswordlessLogin" CssClass="btn btn-outline-info btn-lg btn-block" runat="server" Text="Passwordless Login" OnClick="btnPasswordlessLogin_Click"/>
          </div>
        </div>
      </div>
    </div>
  </div>
</section>
<section id="displaymodal" runat="server">
                    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                      <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-content">
                                <div class="card-body text-center"> 
                                    <img src="Images/TechStoreLogo.png" height="60" width="200" />
                                    
                                    <div class="comment-box text-center">
                                        <br />
                                        <div>
                                            <br />
                                            <div style="display:flex;flex-wrap:wrap; margin-right:auto;margin-left:auto;display:inline;">
                                                <img src="Images/successSecQuesTick.png" height="100" width="100" />
                                                <img src="Images/secuirtyquestionsdisplay.png" height="100" width="100" />
                                            </div>
                                            <br />
                                        </div>
                                        <br />
                                        <br />
                                        <h4>This account has been suspended due to suspicious activities</h4>
                                        <br />
                                        <h5>However, we recognise this account to have Security Question enabled.</h5>
                                        <br />
                                        <h5>You can choose to recover your account by answering a security question or via email</h5>
                                </div>
                            </div>
                        </div>
                          <div class="modal-body">

                          </div>
                          <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                            <asp:Button ID="btnRedirectAnswerSecurityQues" CssClass="btn btn-primary" runat="server" OnClick="btnRedirectAnswerSecurityQues_Click" Text="Answer Now" />
                          </div>
                        </div>
                      </div>
                    </div>
        </section>
</asp:Content>
