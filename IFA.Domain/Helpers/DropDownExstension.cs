using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace IFA.Domain.Helpers
{
    public static class DropDownExstension
    {
        public static SelectList ToDropDownList(this List<DropDownListItem> list, string selectedValue = "", string defaultText = "", bool isOrderByText = true, bool isDefaultTextAdded = true, bool isGrouped = false, IEnumerable<string> disabledValue = null)
        {
            if (list == null)
            {
                list = new List<DropDownListItem>();
            }

            if (isOrderByText)
            {
                list = list.OrderBy(e => e.Text).ToList();
            }

            if (string.IsNullOrEmpty(defaultText))
            {
                defaultText = "Silahkan Pilih";
            }


            if (isDefaultTextAdded)
            {
                list.Insert(0, new DropDownListItem() { Text = defaultText, Value = string.Empty });
            }

            if (!string.IsNullOrEmpty(selectedValue))
            {
                if (list.Count(e => e.Value.ToLower() == selectedValue.ToLower()) == 1)
                {
                    if (isGrouped)
                    {
                        //return new SelectList(list, "Value", "Text", selectedValue, "Group", null, disabledValue);
                             return new SelectList(list, "Value", "Text", selectedValue, "Group", null, disabledValue);
                    }
                    else
                    {
                        return new SelectList(list, "Value", "Text", null, selectedValue, disabledValue);
                    }
                }
            }

            if (isGrouped)
            {
                return new SelectList(list, "Value", "Text", "Group", null, disabledValue);
            }
            else
            {
                return new SelectList(list, "Value", "Text", null, null, disabledValue);
            }

        }
        public static SelectList ToSelectList<T>(this List<T> list, string name = "Name", string value = "ID", string selectedValue = "", string defaultText = "", bool isDefaultTextAdded=true)
        {
            if (list == null)
            {
                list = new List<T>();
            }

            List<DropDownListItem> listItems = list.ToDropDownListItems<T>("{0}", new string[] { name }, value);
            return listItems.ToDropDownList(selectedValue, defaultText, true,isDefaultTextAdded);
        }

        private static List<DropDownListItem> ToDropDownListItems<T>(this List<T> list, string format, string[] names, string value)
        {
            List<DropDownListItem> listItems = new List<DropDownListItem>();

            if (list != null && list.Count() > 0)
            {
                PropertyInfo[] propertyInfos = null;

                T firstItem = list.FirstOrDefault();
                if (firstItem != null)
                {
                    propertyInfos = firstItem.GetType().GetProperties();
                }

                PropertyInfo[] propertyInfoNames = new PropertyInfo[names.Length];
                PropertyInfo propertyInfoValue = null;

                //check if the properties exist
                if (propertyInfos != null && propertyInfos.Length > 0)
                {
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        if (propertyInfo.Name == value)
                            propertyInfoValue = propertyInfo;
                    }

                    for (int i = 0; i < names.Length; i++)
                    {
                        string name = names[i];

                        foreach (PropertyInfo propertyInfo in propertyInfos)
                        {
                            if (propertyInfo.Name == name)
                                propertyInfoNames[i] = propertyInfo;
                        }
                    }
                }

                //build the drop down list item if the matching property are found
                if (propertyInfoNames != null && propertyInfoNames.Length > 0 && propertyInfoValue != null)
                {
                    foreach (object item in list)
                    {
                        if (item != null)
                        {
                            DropDownListItem listItem = new DropDownListItem();

                            string[] nameValues = new string[propertyInfoNames.Length];
                            for (int i = 0; i < nameValues.Length; i++)
                            {
                                if (propertyInfoNames[i] != null)
                                {
                                    nameValues[i] = propertyInfoNames[i].GetValue(item, null).ToString();
                                }
                            }

                            listItem.Text = String.Format(format, nameValues);

                            if (propertyInfoValue.PropertyType.IsEnum)
                            {
                                listItem.Value = Convert.ToInt32(propertyInfoValue.GetValue(item, null)).ToString();
                            }
                            else
                            {
                                listItem.Value = propertyInfoValue.GetValue(item, null).ToString();
                            }
                            listItems.Add(listItem);
                        }
                    }
                }
            }

            return listItems;
        }
    }
}
