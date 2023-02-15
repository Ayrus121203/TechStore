<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminViewProduct.aspx.cs" Inherits="TechStore.AdminEditProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="page-header">
    <h2>View All Products</h2>
    <p>You can choose to edit/delete products here</p>
        </section>

    <section id="product1" class="section-p1">
        <div class="pro-container">
            <asp:Repeater ID="rptrProducts" runat="server">
                <ItemTemplate>
                        <div class="pro">
                            <img src="Images/ProductImages/<%#Eval("ProductID") %>/<%#Eval("Name") %><%#Eval("Extension") %>" alt="<%#Eval("Name") %>">
                            <div class="des">
                                <span><%#Eval("ProdBrand") %></span>
                                <h5><%#Eval("ProdName") %></h5>
                                <h4><div class="proPrice"><span class="proOgPrice"><%#Eval("ProdUsualPrice") %></span> <%#Eval("ProdSellPrice") %> <span class="proPriceDiscount">(<%#Eval("ProdDiscount") %>  Off)</span></div></h4>
                            </div>
                            <a href="DeleteProduct?ProductID=<%#Eval("ProductID") %>"><i class="fa-solid fa-xmark" style="padding-right:15px"></i></a>
                            <a href="EditProduct?ProductID=<%#Eval("ProductID") %>"><i class="fa-solid fa-pen-to-square" style="padding-left:15px;"></i></a>
                        </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>
</asp:Content>
