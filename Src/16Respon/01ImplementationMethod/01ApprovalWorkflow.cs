namespace _16Respon
{
    // 采购请求
    public class PurchaseRequest
    {
        public string Purpose { get; set; }
        public decimal Amount { get; set; }
        public string Requester { get; set; }

        public PurchaseRequest(string purpose, decimal amount, string requester)
        {
            Purpose = purpose;
            Amount = amount;
            Requester = requester;
        }
    }

    // 抽象审批者
    public abstract class Approver
    {
        protected Approver? _nextApprover;
        protected decimal _maxApprovalAmount;
        protected string _title;

        protected Approver(string title, decimal maxApprovalAmount)
        {
            _title = title;
            _maxApprovalAmount = maxApprovalAmount;
        }

        public void SetNext(Approver nextApprover)
        {
            _nextApprover = nextApprover;
        }

        public virtual void HandleRequest(PurchaseRequest request)
        {
            if (CanApprove(request))
            {
                Approve(request);
            }
            else if (_nextApprover != null)
            {
                Console.WriteLine($"  {_title}: 金额超出我的审批权限(¥{_maxApprovalAmount:N0})，转交上级处理");
                _nextApprover.HandleRequest(request);
            }
            else
            {
                Console.WriteLine($"  {_title}: 金额太大，需要董事会审批!");
            }
        }

        protected virtual bool CanApprove(PurchaseRequest request)
        {
            return request.Amount <= _maxApprovalAmount;
        }

        protected virtual void Approve(PurchaseRequest request)
        {
            Console.WriteLine($"  {_title}: 批准了{request.Requester}的采购请求");
            Console.WriteLine($"    用途: {request.Purpose}");
            Console.WriteLine($"    金额: ¥{request.Amount:N0}");
        }
    }

    // 团队领导（可审批1000元以内）
    public class TeamLeaderApprover : Approver
    {
        public TeamLeaderApprover() : base("团队领导", 1000)
        {
        }
    }

    // 部门经理（可审批10000元以内）
    public class ManagerApprover : Approver
    {
        public ManagerApprover() : base("部门经理", 10000)
        {
        }

        protected override void Approve(PurchaseRequest request)
        {
            base.Approve(request);
            Console.WriteLine($"    备注: 请注意成本控制");
        }
    }

    // 总监（可审批50000元以内）
    public class DirectorApprover : Approver
    {
        public DirectorApprover() : base("总监", 50000)
        {
        }

        protected override bool CanApprove(PurchaseRequest request)
        {
            // 总监需要额外检查
            if (request.Amount > 30000)
            {
                Console.WriteLine($"  {_title}: 金额较大，需要仔细审核...");
            }
            return base.CanApprove(request);
        }
    }

    // CEO（可审批100000元以内）
    public class CEOApprover : Approver
    {
        public CEOApprover() : base("CEO", 100000)
        {
        }

        protected override void Approve(PurchaseRequest request)
        {
            Console.WriteLine($"  {_title}: 这是一笔重要支出，我亲自批准");
            base.Approve(request);
            Console.WriteLine($"    批准时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }
    }
}