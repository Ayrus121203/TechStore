<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="UserOrderHistory.aspx.cs" Inherits="TechStore.UserOrderHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.13.1/css/all.min.css" integrity="sha256-2XFplPlrFClt0bIdPgpz8H7ojnk10H69xRqd9+uTShA=" crossorigin="anonymous" />

<div class="container-xl px-4 mt-4">
    <!-- Account page navigation-->
    <nav class="nav nav-borders">
        <a class="nav-link active ms-0" href="Profile">Profile</a>
        <a class="nav-link" href="OrderHistory">Orders</a>
    </nav>
    <hr class="mt-0 mb-4">
    <!-- Payment methods card-->

    <!-- Billing history card-->
    <div class="card mb-4">
        <div class="card-header">Billing History</div>
        <div class="card-body p-0">
            <!-- Billing history table-->
            <div class="table-responsive table-billing-history">
                <table class="table mb-0">
                    <thead>
                        <tr>
                            <th class="border-gray-200" scope="col">Invoice ID</th>
                            <th class="border-gray-200" scope="col">Date</th>
                            <th class="border-gray-200" scope="col">Amount</th>
                            <th class="border-gray-200" scope="col">Status</th>
                            <th class="border-gray-200" scope="col"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptrOrders" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>#<%#Eval("InvoiceID") %></td>
                                    <td><%#Eval("DateOfOrder") %></td>
                                    <td><%#Eval("OrderAmt") %></td>
                                    <td><span class="badge bg-success text-light"><%#Eval("OrderStatus") %></span></td>
                                    <td><a href="UserOrderDetail?InvoiceID=<%#Eval("InvoiceID") %>"><i class="fas fa-file-invoice-dollar"></i></a></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>
</asp:Content>
