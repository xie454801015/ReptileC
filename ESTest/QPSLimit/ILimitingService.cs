using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPSLimit
{
    public interface ILimitingService :IDisposable
    {
        /// <summary>
        /// 申请流量处理
        /// </summary>
        /// <returns>true：获取成功，false：获取失败</returns>
        bool Request();
    }
}
