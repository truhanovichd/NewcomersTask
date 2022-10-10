using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using NewcomersTask.Models;
using NewcomersTask.Web.Models;

namespace NewcomersTask.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderSagaController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderSagaController> _logger;

        public OrderSagaController(
            IBus bus,
            IMapper mapper,
            ILogger<OrderSagaController> logger)
        {
            _bus = bus;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task CreateAsync(CreateOrderRequest model)
        {
            _logger.LogInformation("Start!");
            var request = _mapper.Map<CreateOrder>(model);
            request.OrderId = Guid.NewGuid();
            var endpoint = await _bus.GetSendEndpoint(new Uri("exchange:order-state?bind=true&queue=order-state"));
            await endpoint.Send(request);

            _logger.LogInformation("End!");
        }

        [HttpPost("update")]
        public async Task StatusChangedAsync(ChangedOrderStatusRequest model)
        {
            _logger.LogInformation("Start!");

            var request = _mapper.Map<OrderStatusChanged>(model);
            var endpoint = await _bus.GetSendEndpoint(new Uri("exchange:order-state?bind=true&queue=order-state"));
            await endpoint.Send(request);

            _logger.LogInformation("End!");
        }

        [HttpPost("cancel")]
        public async Task StatusChangedAsync(CancelOrderRequest model)
        {
            _logger.LogInformation("Start!");

            var request = _mapper.Map<OrderCancelled>(model);
            var endpoint = await _bus.GetSendEndpoint(new Uri("exchange:order-state?bind=true&queue=order-state"));
            await endpoint.Send(request);

            _logger.LogInformation("End!");
        }
    }
}