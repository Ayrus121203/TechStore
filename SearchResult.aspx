<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="SearchResult.aspx.cs" Inherits="TechStore.SearchResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="page-header" class="about-header">
        <h2>All Search Results</h2>
        <h3>You searched for <span id="searchtxt" runat="server"></span></h3>
    </section>

    <section class="section-p1">
        <div id="flashes" runat="server" visible="false" style="width:40%; height:40%; display:block; margin-right:auto; margin-left:auto;">
            <img src="Images/SearchNotFound.png" />
        </div>
    </section>

    <section id="product1" class="section-p1">
        <div class="pro-container" id="prodresults" runat="server">
            <asp:Repeater ID="rptrProdsSearchResult" runat="server">
                <ItemTemplate>
                    <a href="ProductView?ProductID=<%#Eval("ProductID") %>">
                        <div class="pro">
                            <img src="Images/ProductImages/<%#Eval("ProductID") %>/<%#Eval("Name") %><%#Eval("Extension") %>" alt="<%#Eval("Name") %>">
                            <div class="des">
                                <span><%#Eval("ProdBrand") %></span>
                                <h5><%#Eval("ProdName") %></h5>
                                <h4><div class="proPrice"><span class="proOgPrice"><%#Eval("UsualPrice") %></span> <%#Eval("SellPrice") %> <span class="proPriceDiscount">(<%#Eval("ProdDiscount") %>  Off)</span></div></h4>
                            </div>
                            <a href="#"><i class="fa-solid fa-cart-shopping"></i></a>
                        </div>
                    </a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>
</asp:Content>
