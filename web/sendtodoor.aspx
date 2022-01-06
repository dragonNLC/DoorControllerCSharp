<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sendtodoor.aspx.cs" Inherits="web.sendtodoor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
<form method="post">
<input value="" name="ip"  /><br />
<input value="" name="dk"  /><br />
<input value="" name="n"  /><br />

<textarea name="json"></textarea>
<%--<input type="submit" value="发送到门" />--%>
<input type="submit" value="获取用户信息" />
</form>



    
</body>
</html>
