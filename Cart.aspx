<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="TechStore.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="page-header" class="about-header">
    </section>
    <section class="section-p1">
    <div id="flashes" runat="server" visible="false" style="width:40%; height:40%; display:block; margin-right:auto; margin-left:auto;">
        <img src="Images/EmptyCart.png" />
    </div>
    </section>
    <div id="cartsec" runat="server">
        <section id="cart" class="section-p1">
        <table width="100%">
            <thead>
                <tr>
                    <td>Remove</td>
                    <td>Image</td>
                    <td>Product</td>
                    <td>Price</td>
                    <td>Quantity</td>
                    <td>Subtotal</td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptrCartProds" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><a href="EditCartItem?ProductID=<%#Eval("ProductID") %>" class="btn btn-danger">Remove</a></td>
                            <td> <img src="Images/ProductImages/<%#Eval("ProductID") %>/<%#Eval("Name") %><%#Eval("Extension") %>" alt="<%#Eval("Name") %>"></td>
                            <td><%#Eval("ProdName") %></td>
                            <td>$ <%#Eval("ProdSellPrice") %></td>
                            <td><%#Eval("CartProdQuantity") %></td>
                            <td>$ <%#Eval("CartProdPrice") %></td>
                        </tr> 
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </section>
    </div>
    <div id="infosec" runat="server">
        <section id="cart-add" class="section-p1">
        <div id="coupon">

        </div>

        <div id="subtotal">
            <h3>Cart Total</h3>
            <table>
                <tr>
                    <td>Cart Subtotal</td>
                    <td id="subtot" runat="server"></td>
                </tr>
                <tr>
                    <td>Delivery Fee</td>
                    <td>$ 4.00</td>
                </tr>
                <tr>
                    <td><strong>Total</strong></td>
                    <td><strong id="tot" runat="server"></strong></td>
                </tr>
            </table>
            <asp:Button ID="btn_ProceedToCheckout" class="btn btn-success" runat="server" Text="Proceed To Checkout" OnClick="btn_ProceedToCheckout_Click" />
        </div>
    </section>
    </div>
</asp:Content>
