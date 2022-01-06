<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="web.mj.home" %>
<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Document</title>
    <link rel="stylesheet" href="./css/bootstrap.min.css">
    <link rel="stylesheet" href="./css2/home.css?">
    <style>
         #L1{position: absolute;left: 22%; top: 78%; }
         #L101{position: absolute;left: 62%; top: 78%; }
         #L2{position: absolute;left: 22%; top: 69%; }
         #L102{position: absolute;left: 62%; top: 69%; }
         #L3{position: absolute;left: 22%; top: 61%; }
         #L103{position: absolute;left: 62%; top: 61%; }
         #L4{position: absolute;left: 22%; top: 54%; }
         #L104{position: absolute;left: 62%; top: 54%; }
         #L5{position: absolute;left: 22%; top: 47%; }
         #L105{position: absolute;left: 62%; top: 47%; }

         #L6{position: absolute;left: 22%; top: 40%; }
         #L106{position: absolute;left: 62%; top: 40%; }

         #L7{position: absolute;left: 22%; top: 34%; }
         #L107{position: absolute;left: 62%; top: 34%; }

         #L8{position: absolute;left: 22%; top: 28%; }
         #L108{position: absolute;left: 62%; top: 28%; }

         #L9{position: absolute;left: 22%; top: 22%; }
         #L109{position: absolute;left: 62%; top: 22%; } 
        

    </style>
</head>

<body>
    <div>
        <div class="bg">
            <%foreach(var l in lclist){ %>
            <a onclick="parent.addnewtab2('/mj/online.aspx?l=<%=l.Key %>','门禁在线管理',4)" id="L<%=l.Key %>" class="btn btn-primary"><%=l.Value %></a>
            <%} %>           
        



        </div>

    </div>

</body>

</html>