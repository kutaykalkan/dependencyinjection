<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="SkyStem.ART.Web.UserControls.ReviewNotesButton" Codebehind="ReviewNotesButton.ascx.cs" %>
<webControls:ExImageButton ID="imgbtnDocument" runat="server" SkinID="ShowCommentPopup"
OnClientClick="return  openReviewNotesWindow(this)" AlternateText="Review Notes" />
