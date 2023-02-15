<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="TechStore.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="page-header">
    <h2>#Stayhome</h2>
    <p>Save more with coupons</p>
        </section>

    <section class="section-p1">
        <div class="filter filter-basic">
          <div class="filter-nav">
              <asp:Button ID="btnAllProds" CssClass="btn btn-success" runat="server" Text="All" OnClick="btnAllProds_Click" />
              <asp:Button ID="btnLowPrice" CssClass="btn btn-success" runat="server" Text="Lowest Price" OnClick="btnLowPrice_Click" />
              <asp:Button ID="btnHighPrice" CssClass="btn btn-success" runat="server" Text="Highest Price" OnClick="btnHighPrice_Click" />
              <asp:Button ID="btnDiscount" CssClass="btn btn-success" runat="server" Text="Best Discount Prices" OnClick="btnDiscount_Click1" />
          </div>
        </div>
    </section>

    <section id="product1" class="section-p1">
        <div class="pro-container">
            <asp:Repeater ID="rptrProducts" runat="server">
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
</asp:Content>
