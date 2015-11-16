using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

namespace MyNet.Tgs.CollectTest
{

    public class InvokeService
    {
        MyNet.Tgs.CollectTest.Interface.TgsInterface tgsinter = new MyNet.Tgs.CollectTest.Interface.TgsInterface();

        public string TestConnect()
        {

            try
            {
                return tgsinter.TgsServiceTest();
            }
            catch (Exception ex)
            {

                return "<error>服务调用错误</error>";
            }

        }
        public string InPassCarInfo(string xmlbody)
        {

            try
            {
                return tgsinter.InPassCarInfo(xmlbody);
            }
            catch (Exception ex)
            {

                return "<error>服务调用错误</error>";
            }

        }

        public DataTable GetDeviceList(string where)
        {

            try
            {
                return tgsinter.GetDeviceList(where);
            }
            catch (Exception ex)
            {

                return null;
            }

        }
    }
}
