using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using vhsConnectMonitor.ServiceModel;

namespace vhsConnectMonitor.ServiceInterface
{
    public class WsInstanceService:Service
    {
        public ISimpleRepository<WsInstance> WsInstanceRepository { get; set; }

        public List<WsInstance> Get(AllWsInstances request)
        {
            return WsInstanceRepository.Select().ToList();
        }


        public WsInstance Get(GetWsInstance request)
        {
            if (string.IsNullOrEmpty(request.ClientId))
            {
                throw new ArgumentException("request.ClientId");
            }

            var result= WsInstanceRepository.Single(ws => ws.ClientId == request.ClientId);

            if (result == null)
            {
                throw HttpError.NotFound("WsInstance with id='{0}' not found.".Fmt(request.ClientId));
            }
            else
            {
                return result;
            }
        }

        public string Get(PingWsInstance request)
        {
            var wsInstance = WsInstanceRepository.Single(ws => ws.ClientId == request.ClientId);

            if (wsInstance == null)
            {
                throw HttpError.NotFound("WsInstance with id='{0}' not found.".Fmt(request.ClientId));
            }

            return "PONG von '{0}'".Fmt(wsInstance.ClientId);

        }
    }


    public interface ISimpleRepository<T>
    {
        IEnumerable<T> Select(Expression<Func<T, bool>> filter=null);

        T Single(Expression<Func<T, bool>> filter);
    }


    public class ListBasedRepository<T>:ISimpleRepository<T>
    {
        private readonly List<T> _instances = new List<T>();

        #region ctor

        public ListBasedRepository(IEnumerable<T> seedValues)
        {
            this._instances.AddRange(seedValues);
        }

        public ListBasedRepository(params T[] seedValues)
        {
            this._instances.AddRange(seedValues);
        }

        #endregion ctor

        public T Single(Expression<Func<T,bool>> filter)
        {
            return _instances.FirstOrDefault(filter.Compile());
        }

        public IEnumerable<T> Select(Expression<Func<T, bool>> filter = null)
        {
            if(filter==null)
            {
                return _instances;
            }
            else
            {
                return _instances.Where(filter.Compile());

            }
        }
    }



}
