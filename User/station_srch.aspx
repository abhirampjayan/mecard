<%@ Page Language="C#" AutoEventWireup="true" CodeFile="station_srch.aspx.cs" Inherits="zonehome" %>

<!--A Design by W3layouts 
Author: W3layout
Author URL: http://w3layouts.com
License: Creative Commons Attribution 3.0 Unported
License URL: http://creativecommons.org/licenses/by/3.0/
-->
<!DOCTYPE html>
<html>
<head>
<title>Online Crime Reporting</title>
<link href="css/bootstrap.css" rel="stylesheet" type="text/css" media="all" />
<!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
<script src="js/jquery.min.js"></script>
<!-- Custom Theme files -->
<!--theme-style-->
<link href="css/style.css" rel="stylesheet" type="text/css" media="all" />	
<!--//theme-style-->
<meta name="viewport" content="width=device-width, initial-scale=1">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="keywords" content="Armor Responsive web template, Bootstrap Web Templates, Flat Web Templates, Andriod Compatible web template, 
Smartphone Compatible web template, free webdesigns for Nokia, Samsung, LG, SonyErricsson, Motorola web design" />
<script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
</head>
<body>
<form runat="server">
<div class="header-top">
	<div class="container">
		<div class="logo">
			<a href="index.aspx"><img src="images/logo.png" alt=""></a>
		</div>
		<div class="top-nav">
				<span class="menu">  </span>
					<ul >
						<li ><a href="zonehome.aspx" > Home </a></li>
											
						<li><a href="logout.aspx" >Logout</a></li>
					<div class="clearfix"> </div>
					</ul>
					<script>
					    $("span.menu").click(function () {
					        $(".top-nav ul").slideToggle(500, function () {
					        });
					    });
					</script>
				</div>	
<div class="clearfix"></div>				
	</div>
</div>


<!--//header-->
<!--content-->
	<div class="content">
		<div class="container">
			
			<div class="content-welcome">
			<div class="col-lg-12"><h3>Welcome<font color="blue"> 
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></font>, Welcome to <font color="green"><u>
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label> </u></font></h3><hr /></div>
			
				<div class="col-md-3 red">
				<a class="btn btn-block btn-lg btn-info" href="station_manage.aspx">Manage Police Station</a>
				<a class="btn btn-block btn-lg btn-info" href="station_srch.aspx">Search Police Station</a>
				<a class="btn btn-block btn-lg btn-info" href="station_complaint.aspx">View Complaints</a>
				<a class="btn btn-block btn-lg btn-info" href="zone_message.aspx">Messages</a>
				</div>
				<div class="col-md-6 come">				
				Choose District <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" 
                                    DataSourceID="SqlDataSource1" DataTextField="dist" DataValueField="dist" onChange="load_station(this.value)">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:online_crimeConnectionString %>" 
                                    SelectCommand="SELECT [dist] FROM [zone_dist] WHERE ([zid] = @zid)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="zoid" Name="zid" PropertyName="Text" 
                                            Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <asp:TextBox ID="zoid" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>	
				<script type="text/javascript">
				    function load_station(x) {
				        if (x != "Choose") {
				            var xhttp = new XMLHttpRequest();
				            xhttp.onreadystatechange = function () {
				                if (xhttp.readyState == 4 && xhttp.status == 200) {
				                    document.getElementById("s_list").innerHTML = xhttp.responseText;
				                }
				            };
				            xhttp.open("GET", "load_station.aspx?st=" + x, true);
				            xhttp.send();
				        }
				        else {
				        }
				    }
					</script>
                    <span id="s_list"></span>    
					
				</div>
                
				<div class="col-md-3 red">
				<h4><font color="red">District List</font></h4><br />
                    <asp:Label ID="Label3" runat="server"></asp:Label>
					
					
				</div>
				<div class="clearfix"> </div>
		
			</div>
			<!---->
			
		<!--footer-->
		<div class="footer">
			<div class="container">
				<div class="footer-top">
					
					<p class="footer-in">Accademic Project </p>
					 <p class="footer-class">Trinity Technologies © 2015  . All Rights Reserved  </p>
					
				</div>
			</div>
		</div>
		<!--//footer-->
		</form>
</body>
</html>