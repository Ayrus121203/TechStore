<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="ProductBrandFilter.aspx.cs" Inherits="TechStore.ProductBrandFilter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="page-header">
    <h2>#Stayhome</h2>
    <p>Save more with coupons</p>
        </section>

    <section id="product1" class="section-p1">
        <div class="pro-container">
            <asp:Repeater ID="rptrProducts" runat="server">
                <ItemTemplate>
                    <a href="ProductView.aspx?ProductID=<%#Eval("ProductID") %>">
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
</asp:Content>
