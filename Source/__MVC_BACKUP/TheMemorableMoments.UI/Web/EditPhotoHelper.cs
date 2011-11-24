using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Chucksoft.Core.Extensions;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.UI.Web
{
    public class EditPhotoHelper
    {
        /// <summary>
        /// Gets the day.
        /// </summary>
        /// <param name="selectedDay">The selected day.</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetDays(int? selectedDay = null)
        {
            IEnumerable<int> daysInMonth = Enumerable.Range(1, 31);
            List<SelectListItem> days = daysInMonth.Select(o => new SelectListItem
            {
                Selected = (selectedDay.GetValueOrDefault() == o),
                Text = o.ToString(),
                Value = o.ToString()
            }).ToList();

            days.Insert(0, new SelectListItem { Text = "", Value = "" });
            return days;
        }


        /// <summary>
        /// Gets the day.
        /// </summary>
        /// <param name="selectedMonth">The selected month.</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetMonths(int? selectedMonth = null)
        {
            IList<Months> daysInMonth = EnumerationsExtensions.GetValues<Months>();

            List<SelectListItem> months = daysInMonth.Select(o => new SelectListItem
            {
                Selected = (selectedMonth.HasValue && selectedMonth.Value == ((int)o)),
                Text = Convert.ToString(o),
                Value = ((int)o).ToString()
            }).ToList();

            months.Insert(0, new SelectListItem { Text = "", Value = "" });

            return months;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetVisibility()
        {
            IEnumerable<SelectListItem> items = new List<SelectListItem>
                                                {
                                                     new SelectListItem {Text = "Public", Value = "Public"},
                                                     new SelectListItem {Text = "In Network", Value = "InNetwork"},
                                                     new SelectListItem {Text = "Private", Value = "Private"},
                                                };

            return items;
        }
    }
}