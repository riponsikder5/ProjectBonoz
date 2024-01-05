namespace BonozAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _orderManager;
        public OrderController(IOrder order)
        {
            _orderManager = order;
        }

        

        [HttpPost]
        public async Task<ActionResult<IList<OrderDTO>>> GetAllOrders(AllOrdersDTO orderDTO)
        {
            try
            {
                var allOrders = _orderManager.GetAllOrders(orderDTO);
                var result = allOrders.Result;
                if (result.Count <= 0)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }


        [HttpGet("{userId}")]
        public async Task<ActionResult<IList<OrderDTO>>> GetOrders(int userId)
        {
            try
            {
                if (userId != 0)
                {
                    var data = await _orderManager.GetOrdersByUser(userId);

                    if (data != null)
                    {
                        return Ok(data);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return BadRequest("User ID cannot be 0.");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("OrdersDetails/{orderId}")]
        public async Task<ActionResult<IList<OrderDetailsDTO>>> GetOrdersDetails(int orderId)
        {
            try
            {
                if (orderId != 0)
                {
                    var data = await _orderManager.GetOrdersDetails(orderId);

                    if (data.Count > 0)
                    {
                        return Ok(data);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return BadRequest("User ID cannot be 0.");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }
        
        [HttpGet]
        [Route("GetOrderById/{orderId}")]
        public async Task<ActionResult<OrderDTO>> GetOrdersById(int orderId)
        {
            try
            {
                if (orderId != 0)
                {
                    var order = await _orderManager.GetOrderById(orderId);
                    if(order != null)
                    {
                        var orderDTO = order.ConvertToDto();

                        if (orderDTO != null)
                        {
                            return Ok(orderDTO);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                   
                }
                else
                {
                    return BadRequest("User ID cannot be 0.");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(OrderDTO orderDTO, int id)
        {
            try
            {
                if (orderDTO != null)
                {
                    var categoryEntity = orderDTO.ConvertToEntity();
                    await _orderManager.UpdateOrder(categoryEntity);

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductCategory(int id)
        {
            try
            {
                if (id > 0)
                {
                    var order = await _orderManager.DeleteOrder(id);
                    if(order)
                    {
                        return Ok();
                    }
                    else 
                    {
                        return NotFound("order not found in database");
                    }
                }
                else
                {
                    return BadRequest("Order Id not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }


        [HttpPost("{cardId}")]
        public async Task<ActionResult> CreateOrder(OrderDTO orderDTO, int cardId)
        {
            try
            {
                if (orderDTO != null && cardId != 0)
                {
                    var order = orderDTO.ConvertToEntity();
                    _orderManager.CreateOrder(order, cardId);

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        #region Hide

        //[HttpGet("{id}")]
        //[Route("GetOrdersDetails")]
        //public async Task<ActionResult<IList<OrderDetailsDTO>>> GetOrdersDetails(int id)
        //{
        //    try
        //    {
        //        if (id != 0)
        //        {
        //            var data = await _orderManager.GetOrdersDetails(id);

        //            if(data != null)
        //            {
        //                return Ok(data);
        //            }
        //            else
        //            {
        //                return NoContent();
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest("User ID cannot be 0.");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //                        "Error retrieving data from the database");
        //    }
        //}
        #endregion
    }
}
