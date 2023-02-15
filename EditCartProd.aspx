<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="EditCartProd.aspx.cs" Inherits="TechStore.EditCartProd" %>
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
            <br />
            <asp:Label ID="availablestock" runat="server" CssClass="text-muted"></asp:Label>
                </div>

        <div class="col-md-7" style="padding-left:20px;">
                    <asp:Repeater ID="rptrProductDetails" runat="server">
                        <ItemTemplate>
                            <div class="single-pro-details">
                                <h6 class="text-muted"><%#Eval("ProdBrand") %></h6>
                                <h4><%#Eval("ProdName") %></h4>
                                <h2>$ <%#Eval("ProdSellPrice") %></h2>
                                <div style="display:flex; padding:5px">
                                 <asp:TextBox Style="width:70px; padding:10px 5px 10px 15px;" ID="chosenquan" TextMode="Number" runat="server"></asp:TextBox>
                                  
                                </div>
                                    <div style="display:flex">
                                     <asp:Button ID="btnRemove" CssClass="btn btn-danger" runat="server" OnClick="btnRemoveFromCart_Click" Text="Remove" />
                                     <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-info" OnClick="btnUpdate_Click" Text="Update" />
                                    </div>
                                <div style="display:flex; flex-wrap:wrap;">
                                    <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
                                </div>
                                    <h4>Product Details</h4>
                                    <span><%#Eval("ProdDes") %></span>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
   </section>
</asp:Content>
