<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="TechStore.UserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script type="text/javascript">
            $(window).on('load', function () {
                $('#myModal').modal('show');
            });
        </script>
<div class="container-xl px-4 mt-4">
    <!-- Account page navigation-->
    <nav class="nav nav-borders">
        <a class="nav-link active ms-0" href="Profile">Profile</a>
        <a class="nav-link" href="OrderHistory">Orders</a>
    </nav>
    <hr class="mt-0 mb-4">
    <div class="row">
        <div class="col-xl-4">
            <!-- Profile picture card-->
            <asp:Repeater ID="rptrUserProfPic" runat="server">
                <ItemTemplate>
                    <div class="card mb-4 mb-xl-0">
                        <div class="card-header">Profile Picture</div>
                        <div class="card-body text-center">
                            <!-- Profile picture image-->
                            <img class="img-account-profile rounded-circle mb-2" src="Images/UserProfilePics/<%#Eval("UserID") %>/<%#Eval("ProfilePicName") %><%#Eval("ProfilePicExtension") %>" alt="" onerror="this.src='Images/userprofDefault.png'" style="width:50%;height:150px;">
                            <!-- Profile picture upload button-->
                            <asp:FileUpload ID="fuimg01" CssClass="form-control"  runat="server" />
                            <br />
                            <asp:Button ID="btnUpdateImg" CssClass="btn btn-primary" runat="server" Text="Update Profile Picture" OnClick="btnUpdateImg_Click" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="col-xl-8">
            <!-- Account details card-->
            <div class="card mb-4">
                <div class="card-header">Account Details</div>
                <div class="card-body">
                    <asp:Repeater ID="rptrUserDetails" runat="server">
                        <ItemTemplate>
                            <!-- Form Group (username)-->
                            <div class="mb-3">
                                <asp:Label ID="Label1" CssClass="small mb-1" runat="server" Text="Username"></asp:Label>
                                <asp:TextBox ID="txtUsername" CssClass="form-control" PlaceHolder="Enter Your Username" Text='<%#Eval("Username") %>' runat="server"></asp:TextBox>
                            </div>
                            <!-- Form Row-->
                            <div class="row gx-3 mb-3">
                                <!-- Form Group (first name)-->
                                <div class="col-md-12">
                                    <asp:Label ID="Label2" CssClass="small mb-1" runat="server" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtName" CssClass="form-control" PlaceHolder="Enter Your Name" Text='<%#Eval("Name") %>' runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <!-- Form Group (email address)-->
                            <div class="mb-3">
                                <asp:Label ID="Label3" CssClass="small mb-1" runat="server" Text="Email"></asp:Label>
                                <asp:TextBox ID="txtEmail" CssClass="form-control" PlaceHolder="Enter Your Email" Text='<%#Eval("Email") %>' runat="server"></asp:TextBox>
                            </div>
                            <!-- Form Row-->
                            <div class="row gx-3 mb-3">
                                <!-- Form Group (phone number)-->
                                <div class="col-md-12">
                                    <asp:Label ID="Label4" CssClass="small mb-1" runat="server" Text="Phone Number"></asp:Label>
                                    <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" PlaceHolder="Enter Your Phone Number" Text='<%#Eval("PhoneNumber") %>' runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <!-- Save changes button-->
                            
                        </ItemTemplate>
                    </asp:Repeater>
                          <div class="row gx-3 mb-3">
                                <!-- Form Group (phone number)-->
                                <div class="col-md-12">
                                    <asp:Label ID="Label4" CssClass="small mb-1" runat="server" Text="Device Address"></asp:Label>
                                    <asp:TextBox ID="txtDeviceAddress" CssClass="form-control" TextMode="Password" PlaceHolder="Enable Device Proximity Authentication" ReadOnly="true" runat="server"></asp:TextBox>
                                </div>

                            </div>
                    <asp:Button ID="btnUpdateUser" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnUpdateUser_Click" />
                    <!-- Button trigger modal -->
                    <button type="button" class="btn btn-danger" data-toggle="modal" data-target="#exampleModal">
                      Delete
                    </button>
                    <button type="button" class="btn btn-secondary" data-toggle="modal" data-target="#deviceproxauthmodal">
                      Device Proximity Auth
                    </button>
                    <%--<asp:Button  CssClass="" runat="server" Text=""/>--%>
                    <asp:Button ID="btnEnableSecurityQues" CssClass="btn btn-secondary" runat="server" Text="Security Question" OnClick="btnEnableSecurityQues_Click" />

                    <!-- Modal -->
                    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                      <div class="modal-dialog" role="document">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">Delete User Account</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                              <span aria-hidden="true">&times;</span>
                            </button>
                          </div>
                          <div class="modal-body">
                            Are you sure you want to delete your account?
                          </div>
                          <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                            <asp:Button ID="btnDeletAcc" CssClass="btn btn-danger" runat="server" Text="Delete Account" OnClick="btnDeletAcc_Click" />
                          </div>
                        </div>
                      </div>
                    </div>

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
                                        <h4>Choose to enable 1 or more security question with us.</h4>
                                        <br />
                                        <h5>This is useful when recovering your account or changing your password</h5>
                                </div>
                            </div>
                        </div>
                          <div class="modal-body">

                          </div>
                          <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Later</button>
                            <asp:Button ID="btnRedirectToSecQues" CssClass="btn btn-primary" runat="server" Text="Lets Do It" OnClick="btnRedirectToSecQues_Click" />
                          </div>
                        </div>
                      </div>
                    </div>
        </section>


                    <div class="modal fade" id="deviceproxauthmodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                      <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-content">
                                <div class="card-body text-center"> 
                                    <img src="Images/TechStoreLogo.png" height="60" width="200" />
                                    
                                    <div class="comment-box text-center">
                                        <br />
                                        <div>
                                            <br />
                                                <img src="Images/proxdevice2FABanner.png" width="450" height="150" />
                                            <br />
                                        </div>
                                        <br />
                                    <br />
                                        <h4>Enable Device Proximity Authentication with us today.</h4>
                                        <br />
                                        <h5>Go passwordless by using your phone bluetooth to sign in</h5>
                                </div>
                            </div>
                        </div>
                          <div class="modal-body">

                          </div>
                          <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Later</button>
                            <asp:Button ID="EnableDeviceProxAuth" CssClass="btn btn-primary" runat="server" Text="Let's Go" OnClick="btnDeviceProxSearch_Click" />
                          </div>
                        </div>
                      </div>
                   </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>