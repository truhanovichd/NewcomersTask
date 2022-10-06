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
        public async Task<OrderResponse> CreateAsync(OrderCreateRequest model)
        {
            _logger.LogInformation("Start!");
            var request = _mapper.Map<OrderSaga>(model);
            request.CorrelationId = Guid.NewGuid();
            var response = await _bus.Request<OrderSaga, OrderResponse>(request);
            _logger.LogInformation("End!");

            return response.Message;
        }

        [HttpPost("update")]
        public async Task<OrderResponse> StatusChangedAsync(OrderStatusChangedRequest model)
        {
            _logger.LogInformation("Start!");

            //TODO update code for update status.
            var request = _mapper.Map<OrderSaga>(model);
            var response = await _bus.Request<OrderSaga, OrderResponse>(request);
            _logger.LogInformation("End!");

            return response.Message;
        }
    }
}