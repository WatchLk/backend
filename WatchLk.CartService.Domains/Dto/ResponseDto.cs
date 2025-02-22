using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchLk.CartService.Domains.Dto
{
    public record ResponseDto
    (
        bool Succeeded,
        string Message,
        List<CartItemDto>? Result
    );
}
