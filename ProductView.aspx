<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="ProductView.aspx.cs" Inherits="TechStore.ProductView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <section id="proddetails" class="section-p1">
            <div class="col-md-5">
                <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
                    <!-- Indicators -->
                    <ol class="carousel-indicators">
                        <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                        <li data-target="#carousel-example-generic" data-slide-to="1"></li>
                        <li data-target="#carousel-example-generic" data-slide-to="2"></li>
                        <li data-target="#carousel-example-generic" data-slide-to="3"></li>
                        <li data-target="#carousel-example-generic" data-slide-to="4"></li>
                    </ol>

                    <!-- Wrapper for slides -->
                    <div class="carousel-inner">
                        
                        <asp:Repeater ID="rptrImages" runat="server">
                            <ItemTemplate>
                                
                                <div class="carousel-item <%# GetActiveClass(Container.ItemIndex) %>">
                                    
                                        <img src="Images/ProductImages/<%#Eval("ProductID") %>/<%#Eval("Name") %><%#Eval("Extension") %>" alt="<%#Eval("Name") %>" onerror="this.src='images/noimage.jpg'" style="width:100%">
                                    </div>
                            </ItemTemplate>
                        </asp:Repeater>
                            
                    </div>
                    <!-- Controls -->
                    <a class="carousel-control-prev" href="#carousel-example-generic" role="button" data-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="sr-only">Previous</span>
                    </a>
                    <a class="carousel-control-next" href="#carousel-example-generic" role="button" data-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="sr-only">Next</span>
                    </a>
                </div>
            </div>
        

            <div class="col-md-7" style="padding-left:20px;">
                <asp:Repeater ID="rptrProductDetails" runat="server">
                    <ItemTemplate>
                        <div class="single-pro-details">
                            <h4 class="text-muted"><%#Eval("ProdCategory") %></h4>
                           <h6 class="text-muted"><%#Eval("ProdBrand") %></h6>
                           <h4><%#Eval("ProdName") %></h4>
                            <div class="star">
                                <asp:Rating ID="TopPageRating" ReadOnly="true" MaxRating="5" runat="server" StarCssClass="Star" WaitingStarCssClass="WaitingStar" FilledStarCssClass="FilledStar" EmptyStarCssClass="Star"></asp:Rating>
                            </div>
                            <br />
                            <h2>$ <%#Eval("ProdSellPrice") %></h2>
                                    <asp:TextBox Style="width:70px; padding:10px 5px 10px 15px;" ID="ProdQuan" TextMode="Number" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnAddToCart" OnClick="btnAddToCart_Click" CssClass="mainButton" runat="server" Text="ADD TO CART" />
                                <div style="display:flex; flex-wrap:wrap;">
                                    <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
                                </div>
                                <asp:Label ID="availablestock" runat="server" CssClass="text-muted"></asp:Label>
                                <h4>Product Details</h4>
                                <span><%#Eval("ProdDes") %></span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
    </section>

    <section id="AddReview" class="section-p1" runat="server">
<h2 class="row d-flex justify-content-center">Enter Your Review</h2>
                  <div class="card-footer py-3 border-0" style="background-color: #f8f9fa;">
                      <asp:Rating ID="UserInputRatings" MaxRating="5" runat="server" StarCssClass="Star" WaitingStarCssClass="WaitingStar" FilledStarCssClass="FilledStar" EmptyStarCssClass="Star"></asp:Rating>
                    <br />
            <div class="d-flex flex-start w-100">
              <div class="form-outline w-100">
                  <asp:TextBox ID="txtreview" Style="background:#fff; width:100%" Placeholder="Your Review" TextMode="MultiLine" Rows="5" runat="server"></asp:TextBox>
              </div>
            </div>
            <div class="float-end mt-2 pt-1">
                <asp:Button ID="btnPostReview" CssClass="btn btn-primary btn-sm" runat="server" Text="Post comment" OnClick="btnPostReview_Click" />
            </div>
          </div>
    </section>


    <section id="ProdReviews" class="section-p1" >
        <h2 class="row d-flex justify-content-center">Product Reviews From Customers</h2>
        <asp:Repeater ID="rptr_ProdReviews" runat="server">
          <ItemTemplate>
            <div class="row d-flex justify-content-center">

              <br />
            <div class="col-md-10">
              <div class="card">
                <div class="card-body m-3">
                  <div class="row">
                    <div class="col-lg-4 d-flex justify-content-center align-items-center mb-4 mb-lg-0">
                      <img src="Images/UserProfilePics/<%#Eval("UserID") %>/<%#Eval("ProfilePicName") %><%#Eval("ProfilePicExtension") %>" onerror="this.src='Images/userprofDefault.png'"
                        class="rounded-circle img-fluid shadow-1" width="200" height="200" />
                    </div>
                    <div class="col-lg-8">
                      <p class="text-muted fw-light mb-4">
                        <%#Eval("review") %>
                      </p>
                      <p class="fw-bold lead mb-2"><strong><%#Eval("Username") %></strong></p>
                      <asp:Rating ID="UserRatings" CurrentRating='<%#Eval("rating") %>' ReadOnly="true" MaxRating="5" runat="server" StarCssClass="Star" WaitingStarCssClass="WaitingStar" FilledStarCssClass="FilledStar" EmptyStarCssClass="Star"></asp:Rating>
                    </div>
                  </div>
                </div>
              </div>
            </div>
        </div>
              <br />
    </ItemTemplate>
        </asp:Repeater>
    </section>
</asp:Content>
