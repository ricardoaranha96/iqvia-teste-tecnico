using TesteTecnico.Areas.Identity.Data;

namespace TesteTecnico.Models
{
    public enum OrderStatuses
    {
        CREATING,
        AWAITING_PAYMENT,
    }

    public class Order
    {
        public int Id { get; set; }

        public string? UserId { get; set; }
        public OrderStatuses Status { get; set; } = OrderStatuses.CREATING;

        public TesteTecnicoUser User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; } = null;

        public ICollection<OrderProduct>? OrdersProducts { get; set; }

        public String getStatusName()
        {
            switch (Status)
            {
                case OrderStatuses.CREATING:
                    return "Em Criação";
                case OrderStatuses.AWAITING_PAYMENT:
                    return "Aguardando Pagamento";
                default:
                    throw new Exception("Status inválido");
            }
        }
    }
}
