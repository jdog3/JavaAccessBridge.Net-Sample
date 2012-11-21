using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

namespace JabApiLib.JavaAccessBridge
{
    public static class JabHelpers
    {
        public class AccessibleTreeItem
        {
            public String name; // the AccessibleName of the object
            public String description; // the AccessibleDescription of the object
            public String role; // localized AccesibleRole string
            public String role_en_US; // AccesibleRole string in the en_US locale
            public String states; // localized AccesibleStateSet string (comma separated)
            public String states_en_US; // AccesibleStateSet string in the en_US locale (comma separated)
            public Int32 indexInParent; // index of object in parent
            public Int32 childrenCount; // # of children, if any
            public Int32 x; // screen coords in pixel
            public Int32 y; // "
            public Int32 width; // pixel width of object
            public Int32 height; // pixel height of object
            public Boolean accessibleComponent; // flags for various additional
            public Boolean accessibleAction; // Java Accessibility interfaces
            public Boolean accessibleSelection; // FALSE if this object doesn't
            public Boolean accessibleText; // implement the additional interface
            public Boolean accessibleInterfaces;
            public string textValue;

            public List<AccessibleTreeItem> children;

            public AccessibleTreeItem()
            {
                children = new List<AccessibleTreeItem>();
            }

            public AccessibleTreeItem(AccessibleContextInfo accessibleContextInfo)
                : this()
            {
                this.name = accessibleContextInfo.name; // the AccessibleName of the object
                this.description = accessibleContextInfo.description; // the AccessibleDescription of the object
                this.role = accessibleContextInfo.role; // localized AccesibleRole string
                this.role_en_US = accessibleContextInfo.role_en_US; // AccesibleRole string in the en_US locale
                this.states = accessibleContextInfo.states; // localized AccesibleStateSet string (comma separated)
                this.states_en_US = accessibleContextInfo.states_en_US; // AccesibleStateSet string in the en_US locale (comma separated)
                this.indexInParent = accessibleContextInfo.indexInParent; // index of object in parent
                this.childrenCount = accessibleContextInfo.childrenCount; // # of children, if any
                this.x = accessibleContextInfo.x; // screen coords in pixel
                this.y = accessibleContextInfo.y; // "
                this.width = accessibleContextInfo.width; // pixel width of object
                this.height = accessibleContextInfo.height; // pixel height of object
                this.accessibleComponent = accessibleContextInfo.accessibleComponent; // flags for various additional
                this.accessibleAction = accessibleContextInfo.accessibleAction; // Java Accessibility interfaces
                this.accessibleSelection = accessibleContextInfo.accessibleSelection; // FALSE if this object doesn't
                this.accessibleText = accessibleContextInfo.accessibleText; // implement the additional interface
                this.accessibleInterfaces = accessibleContextInfo.accessibleInterfaces;
            }

            public Rectangle Bounds
            {
                get
                {
                    return new Rectangle(this.x, this.y, this.width, this.height);
                }
            }

            public Int32 SquarePixels
            {
                get { return this.x * this.y; }
            }
        }


        internal class ItemComparer : IComparer<AccessibleTreeItem>
        {
            #region IComparer<IReader> Members

            public int Compare(AccessibleTreeItem x, AccessibleTreeItem y)
            {
                if (x.SquarePixels < y.SquarePixels)
                {
                    return 1;
                }
                else if (x.SquarePixels > y.SquarePixels)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }

            #endregion
        }

        private static List<AccessibleTreeItem> screenContents = new List<AccessibleTreeItem>();
        public static string screenContentsString = string.Empty;

        public static AccessibleTreeItem GetComponentTree(IntPtr hWnd, out Int32 vmID)
        {
            screenContents.Clear();
            screenContentsString = string.Empty;

            AccessibleTreeItem accessibleTreeItem = new AccessibleTreeItem();
            vmID = 0;
            if (JabApi.isJavaWindow(hWnd) == 1)
            {
                unsafe
                {
                    IntPtr acPtr;

                    if (JabApi.getAccessibleContextFromHWND(hWnd, out vmID, out acPtr))
                    {

                        AccessibleContextInfo ac = new AccessibleContextInfo();
                        accessibleTreeItem = GetAccessibleContextInfo(vmID, acPtr, out ac, null, 0, string.Empty); // RECURSION SEED

                        return accessibleTreeItem;
                    }
                }
            }

            return null;
        }


        public static AccessibleContextInfo GetAccessibleContextInfo(Int32 vmID, IntPtr ac)
        {
            AccessibleContextInfo aci = new AccessibleContextInfo(); //This will be the info that we return.

            // Allocate global memory space for the size of AccessibleContextInfo and store the address in acPtr
            IntPtr acPtr = Marshal.AllocHGlobal(Marshal.SizeOf(new AccessibleContextInfo()));
            try
            {
                //Convert the pointer to an actual object.
                Marshal.StructureToPtr(new AccessibleContextInfo(), acPtr, true);
                //getAccessibleContextInfo() returns true or false, depending on whether it succeeds.
                if (JabApi.getAccessibleContextInfo(vmID, ac, acPtr))
                {
                    aci = (AccessibleContextInfo)Marshal.PtrToStructure(acPtr, typeof(AccessibleContextInfo));
                    //Check that the info isn't null, and then return.
                    if (!ReferenceEquals(aci, null))
                    {
                        return aci;
                    }
                }
            }
            catch
            {
                return new AccessibleContextInfo();
            }
            return new AccessibleContextInfo();
        }

        public static AccessibleTextItemsInfo GetAccessibleTextInfo(Int32 vmID, IntPtr ac)
        {
            //Reserve memory
            IntPtr ati = Marshal.AllocHGlobal(Marshal.SizeOf(new AccessibleTextItemsInfo()));
            //Call DLL.
            JabApi.getAccessibleTextItems(vmID, ac, ati, 0);
            //Creat object
            AccessibleTextItemsInfo atInfo = (AccessibleTextItemsInfo)Marshal.PtrToStructure(ati, typeof(AccessibleTextItemsInfo));
            //Free memory       
            if (ati != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(ati);
            }
            return atInfo;
        }

        private static AccessibleTreeItem GetAccessibleContextInfo(Int32 vmID, IntPtr currentPtr, out AccessibleContextInfo currentContext, AccessibleTreeItem parentItem, int level, string lineage)
        {
            unsafe
            {
                // Allocate global memory space for the size of AccessibleContextInfo and store the address in acPtr
                IntPtr acPtr = Marshal.AllocHGlobal(Marshal.SizeOf(new AccessibleContextInfo()));
                try
                {

                    Marshal.StructureToPtr(new AccessibleContextInfo(), acPtr, true);
                    if (JabApi.getAccessibleContextInfo(vmID, currentPtr, acPtr))
                    {
                        currentContext = (AccessibleContextInfo)Marshal.PtrToStructure(acPtr, typeof(AccessibleContextInfo));
                        if (!ReferenceEquals(currentContext, null))
                        {
                            AccessibleTreeItem newItem = BuildAccessibleTree(currentContext, parentItem);

                            if (!ReferenceEquals(newItem, null))
                            {
                                //Checks to see if current object has any text items.
                                if (currentContext.accessibleText == true)
                                {
                                    AccessibleTextItemsInfo textItem;
                                    //Gets text items.
                                    textItem = GetAccessibleTextInfo(vmID, currentPtr);
                                    newItem.textValue = textItem.sentence;

                                    string treeInfo = Repeat("\t", level) + currentContext.name + " = \"" + textItem.sentence + "\"";
                                    screenContentsString += treeInfo + Environment.NewLine;
                                    Debug.Print(treeInfo);
                                }
                                else
                                {
                                    string treeInfo = Repeat("\t", level) + currentContext.name;
                                    screenContentsString += treeInfo + Environment.NewLine;
                                    Debug.Print(treeInfo);
                                }


                                //Start collecting children
                                int nextLevel = level + 1;
                                for (int i = 0; i < currentContext.childrenCount; i++)
                                {
                                    string lineageInfo = Repeat("\t", level) + level.ToString() + " Child " + i.ToString() + " Lineage = {" + lineage + "}";
                                    screenContentsString += lineageInfo + Environment.NewLine;
                                    Debug.Print(lineageInfo);

                                    string currentlineage;
                                    if (lineage == string.Empty)
                                        currentlineage = i.ToString();
                                    else
                                        currentlineage = lineage + ", " + i.ToString();

                                    if (currentContext.role_en_US != "unknown" && currentContext.states_en_US.Contains("visible")) // Note the optomization here, I found this get me to an acceptable speed
                                    {
                                        AccessibleContextInfo childContext = new AccessibleContextInfo();
                                        IntPtr childPtr = JabApi.getAccessibleChildFromContext(vmID, currentPtr, i);

                                        GetAccessibleContextInfo(vmID, childPtr, out childContext, newItem, nextLevel, currentlineage);

                                        //TODO: Not sure when or where to release the java objects, the JVM will leak memory until released
                                        //JabApi.releaseJavaObject(vmID, childPtr);
                                    }

                                }
                            }

                            return newItem;
                        }
                    }
                    else
                    {
                        currentContext = new AccessibleContextInfo();
                    }
                }
                finally
                {
                    if (acPtr != IntPtr.Zero)
                        Marshal.FreeHGlobal(acPtr);

                    //TODO: Not sure when or where to release the java objects, the JVM will leak memory until released
                    //JabApi.releaseJavaObject(vmID, currentPtr);
                }
            }
            return null;
        }

        public static string Repeat(string s, int count)
        {
            var _s = new System.Text.StringBuilder().Insert(0, s, count).ToString();
            return _s;
        }


        private static AccessibleTreeItem BuildAccessibleTree(AccessibleContextInfo acInfo, AccessibleTreeItem parentItem)
        {
            if (!ReferenceEquals(acInfo, null))
            {
                AccessibleTreeItem item = new AccessibleTreeItem(acInfo);
                if (!ReferenceEquals(parentItem, null))
                {
                    screenContents.Add(item);
                    parentItem.children.Add(item);
                }
                return item;
            }
            return null;
        }
    }
}
