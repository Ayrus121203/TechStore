<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="ForgotPasswordVerifyEmail.aspx.cs" Inherits="TechStore.ForgotPasswordVerifyEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script type="text/javascript">
            $(window).on('load', function () {
                $('#myModal').modal('show');
            });
        </script>
    <section class="section-p1">
 <div class="form-gap"></div>
<div class="container" style="margin:0 auto">
	<div class="row">
		<div class="col-md-4 col-md-offset-4">
            <div class="panel panel-default">
              <div class="panel-body">
                <div class="text-center">
                  <h3><i class="fa fa-lock fa-4x"></i></h3>
                  <h2 class="text-center">Forgot Password?</h2>
                  <p>You can reset your password here.</p>
                  <div class="panel-body">
   
                      <div class="form-group">
                        <div class="input-group">
                          <span class="input-group-addon"><i class="glyphicon glyphicon-envelope color-blue"></i></span>
                          <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                      </div>
                      <div class="form-group">
                          <asp:Button ID="btnVerify" runat="server" Text="Verify Email" OnClick="btnVerify_Click" CssClass="btn btn-lg btn-primary btn-block" />
                      </div>
                      <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                  </div>
                </div>
              </div>
            </div>
          </div>
	</div>
</div>
</section>

<%-- Modal --%>
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
                                                <img src="Images/secuirtyquestionsdisplay.png" height="180" width="200" />
                                            <br />
                                        </div>
                                        <br />
                                        <br />
                                        <h4>If you have enabled Security Questions on your account,</h4>
                                        <br />
                                        <h5>You can reset your password by simply answering a security question.</h5>
                                        <br />
                                        <h5>If you have not, please 'Close' this pop-up and reset your password via email.</h5>
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
