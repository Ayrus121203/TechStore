<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TechStore.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- New "hero" --%>
    <section id="hero">
        <h4>Trade-in offer</h4>
        <h2>Super value deals</h2>
        <h1>On all products</h1>
        <p>Save more with coupins</p>
        <button>Shop now</button>
        
    </section>
    
    <section id="feature" class="section-p1">
        <div class="fe-box">
            <img src="Images/features/f1.png" alt="" />
            <h6>Free Shipping</h6>
        </div>
        <div class="fe-box">
            <img src="Images/features/f2.png" alt="" />
            <h6>Online Order</h6>
        </div>
        <div class="fe-box">
            <img src="Images/features/f3.png" alt="" />
            <h6>Save Money</h6>
        </div>
        <div class="fe-box">
            <img src="Images/features/f4.png" alt="" />
            <h6>Promotions</h6>
        </div>
        <div class="fe-box">
            <img src="Images/features/f5.png" alt="" />
            <h6>Happy Sell</h6>
        </div>
        <div class="fe-box">
            <img src="Images/features/f6.png" alt="" />
            <h6>Support</h6>
        </div>
    </section>

    <%--href="ProductView.aspx?PID=<%#Eval("PID") %>">--%>
    <section id="product1" class="section-p1">
        <h2>Featured Products</h2>
        <p>Summer collections New Design</p>
        <div class="pro-container">
            <asp:Repeater ID="rptrFeaturedProd" runat="server">
                <ItemTemplate>
                    <a href="ProductView?ProductID=<%#Eval("ProductID") %>">
                        <div class="pro">
                            <img src="Images/ProductImages/<%#Eval("ProductID") %>/<%#Eval("Name") %><%#Eval("Extension") %>" alt="<%#Eval("Name") %>">
                            <div class="des">
                                <span><%#Eval("ProdBrand") %></span>
                                <h5><%#Eval("ProdName") %></h5>
                                <h4><div class="proPrice"><span class="proOgPrice"><%#Eval("ProdUsualPrice") %></span> <%#Eval("ProdSellPrice") %> <span class="proPriceDiscount">(<%#Eval("ProdDiscount") %>  Off)</span></div></h4>
                            </div>
                            <a href="#"><i class="fa-solid fa-cart-shopping"></i></a>
                        </div>
                    </a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section> 

        
    <section id="product1" class="section-p1">
        <h2>New Products</h2>
        <p>New Arrivals</p>
        <div class="pro-container">
            <asp:Repeater ID="rptrNew8Prod" runat="server">
                <ItemTemplate>
                    <a href="ProductView?ProductID=<%#Eval("ProductID") %>">
                        <div class="pro">
                            <img src="Images/ProductImages/<%#Eval("ProductID") %>/<%#Eval("Name") %><%#Eval("Extension") %>" alt="<%#Eval("Name") %>">
                            <div class="des">
                                <span><%#Eval("ProdBrand") %></span>
                                <h5><%#Eval("ProdName") %></h5>
                                <div class="star">
                                    
                                </div>
                                <h4><div class="proPrice"><span class="proOgPrice"><%#Eval("ProdUsualPrice") %></span> <%#Eval("ProdSellPrice") %> <span class="proPriceDiscount">(<%#Eval("ProdDiscount") %>  Off)</span></div></h4>
                            </div>
                            <a href="#"><i class="fa-solid fa-cart-shopping"></i></a>
                        </div>
                    </a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>
        
    <section id="banner3">
        <div class="banner-box">
            <h2>EARING COLLECTION</h2>
            <h3></h3>
        </div>
        <div class="banner-box banner-box2">
            <h2>JACKET COLLECTION</h2>
       
        </div>
        <div class="banner-box banner-box3">
            <h2>CLOTHING COLLECTION</h2>

        </div>
    </section>

</asp:Content>
