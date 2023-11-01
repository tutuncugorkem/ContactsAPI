using Contacts.Core.DTOs;
using Contacts.Core.Models;
using Contacts.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactsAPI.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity   //id ye erişmek için bunu verdik
    {

        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();

            if (idValue == null)
            {
                await next.Invoke(); //yoluna devam et
                return;
            }
            var id = (int)idValue; //varsa int e cast ettik!

            var anyEntity = await _service.AnyAsync(x => x.Id==id);
        
            if(anyEntity)
            {
                await next.Invoke();
                return;
            }
            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name}({id}) not found"));
        }
    }
}

//EĞER BİR FİLTER, CONSTRACTORUNDA BİR SERVİSİ VEYA CLASSI DI OLARAK GEÇİYORSA BUNU PROGRAM CS'DE TANITMAMIZ LAZIM