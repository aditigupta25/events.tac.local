﻿using events.tac.local.Models;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace events.tac.local.Controllers
{
    public class OverViewController : Controller
    {
        // GET: OverView
        public ActionResult Index()
        {
            var model = new OverviewList()
            {
                ReadMore = Sitecore.Globalization.Translate.Text("Read More")

            };
            model.AddRange(RenderingContext.Current.ContextItem.GetChildren(Sitecore.Collections.ChildListOptions.SkipSorting)
                .Select(i => new OverviewItem()
                {
                    url = LinkManager.GetItemUrl(i),
                    Title = new HtmlString(FieldRenderer.Render(i, "contentheading")),
                    Image = new HtmlString(FieldRenderer.Render(i, "decorationbanner", "mw=500&mh=333"))

                }));
            return View(model);
        }
    }
}
