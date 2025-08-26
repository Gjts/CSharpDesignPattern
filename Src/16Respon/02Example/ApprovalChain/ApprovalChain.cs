namespace _16Respon._02Example.ApprovalChain
{
    // 请求类
    public class PurchaseRequest
    {
        public string RequestId { get; }
        public string Description { get; }
        public decimal Amount { get; }
        public string Requester { get; }
        public DateTime RequestDate { get; }

        public PurchaseRequest(string requestId, string description, decimal amount, string requester)
        {
            RequestId = requestId;
            Description = description;
            Amount = amount;
            Requester = requester;
            RequestDate = DateTime.Now;
        }
    }

    // 抽象处理者
    public abstract class Approver
    {
        protected Approver? _nextApprover;
        protected string _name;
        protected decimal _approvalLimit;

        public Approver(string name, decimal approvalLimit)
        {
            _name = name;
            _approvalLimit = approvalLimit;
        }

        public void SetNext(Approver nextApprover)
        {
            _nextApprover = nextApprover;
        }

        public virtual void ProcessRequest(PurchaseRequest request)
        {
            Console.WriteLine($"\n📋 {_name} 正在审核采购申请:");
            Console.WriteLine($"   申请ID: {request.RequestId}");
            Console.WriteLine($"   申请人: {request.Requester}");
            Console.WriteLine($"   描述: {request.Description}");
            Console.WriteLine($"   金额: ¥{request.Amount:N2}");

            if (CanApprove(request))
            {
                Approve(request);
            }
            else if (_nextApprover != null)
            {
                Console.WriteLine($"   ⏩ 金额超出权限(¥{_approvalLimit:N2})，转交上级审批");
                _nextApprover.ProcessRequest(request);
            }
            else
            {
                Console.WriteLine($"   ❌ 金额过大，需要董事会审批");
            }
        }

        protected virtual bool CanApprove(PurchaseRequest request)
        {
            return request.Amount <= _approvalLimit;
        }

        protected abstract void Approve(PurchaseRequest request);
    }

    // 具体处理者 - 部门主管
    public class DepartmentManager : Approver
    {
        public DepartmentManager() : base("部门主管", 5000) { }

        protected override void Approve(PurchaseRequest request)
        {
            Console.WriteLine($"   ✅ {_name}批准了采购申请");
            Console.WriteLine($"   批准金额: ¥{request.Amount:N2}");
            Console.WriteLine($"   批准时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }
    }

    // 具体处理者 - 部门总监
    public class DepartmentDirector : Approver
    {
        public DepartmentDirector() : base("部门总监", 20000) { }

        protected override void Approve(PurchaseRequest request)
        {
            Console.WriteLine($"   ✅ {_name}批准了采购申请");
            Console.WriteLine($"   批准金额: ¥{request.Amount:N2}");
            Console.WriteLine($"   批准时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            
            if (request.Amount > 10000)
            {
                Console.WriteLine($"   📝 备注: 金额较大，需要财务部门跟进");
            }
        }
    }

    // 具体处理者 - 副总裁
    public class VicePresident : Approver
    {
        public VicePresident() : base("副总裁", 50000) { }

        protected override void Approve(PurchaseRequest request)
        {
            Console.WriteLine($"   ✅ {_name}批准了采购申请");
            Console.WriteLine($"   批准金额: ¥{request.Amount:N2}");
            Console.WriteLine($"   批准时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"   📝 备注: 需要签署正式采购合同");
        }

        protected override bool CanApprove(PurchaseRequest request)
        {
            // 副总裁额外检查
            if (request.Amount > 30000)
            {
                Console.WriteLine($"   🔍 需要额外审核...");
                Thread.Sleep(500);
            }
            return base.CanApprove(request);
        }
    }

    // 具体处理者 - 总裁
    public class President : Approver
    {
        public President() : base("总裁", 100000) { }

        protected override void Approve(PurchaseRequest request)
        {
            Console.WriteLine($"   ✅ {_name}批准了采购申请");
            Console.WriteLine($"   批准金额: ¥{request.Amount:N2}");
            Console.WriteLine($"   批准时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"   📝 备注: 重大采购，需要法务部门参与");
            Console.WriteLine($"   📝 备注: 需要进行供应商评估");
        }
    }

    // 请假请求类
    public class LeaveRequest
    {
        public string EmployeeId { get; }
        public string EmployeeName { get; }
        public int Days { get; }
        public string Reason { get; }
        public DateTime StartDate { get; }

        public LeaveRequest(string employeeId, string employeeName, int days, string reason, DateTime startDate)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            Days = days;
            Reason = reason;
            StartDate = startDate;
        }
    }

    // 请假审批抽象处理者
    public abstract class LeaveApprover
    {
        protected LeaveApprover? _nextApprover;
        protected string _title;
        protected int _maxDays;

        public LeaveApprover(string title, int maxDays)
        {
            _title = title;
            _maxDays = maxDays;
        }

        public void SetNext(LeaveApprover nextApprover)
        {
            _nextApprover = nextApprover;
        }

        public virtual void ProcessLeaveRequest(LeaveRequest request)
        {
            Console.WriteLine($"\n📅 {_title} 正在审核请假申请:");
            Console.WriteLine($"   员工: {request.EmployeeName} ({request.EmployeeId})");
            Console.WriteLine($"   请假天数: {request.Days}天");
            Console.WriteLine($"   开始日期: {request.StartDate:yyyy-MM-dd}");
            Console.WriteLine($"   请假原因: {request.Reason}");

            if (CanApprove(request))
            {
                ApproveLeave(request);
            }
            else if (_nextApprover != null)
            {
                Console.WriteLine($"   ⏩ 请假天数超出权限({_maxDays}天)，转交上级审批");
                _nextApprover.ProcessLeaveRequest(request);
            }
            else
            {
                Console.WriteLine($"   ❌ 请假天数过长，需要特别审批");
            }
        }

        protected virtual bool CanApprove(LeaveRequest request)
        {
            return request.Days <= _maxDays;
        }

        protected abstract void ApproveLeave(LeaveRequest request);
    }

    // 具体处理者 - 组长
    public class TeamLeader : LeaveApprover
    {
        public TeamLeader() : base("组长", 2) { }

        protected override void ApproveLeave(LeaveRequest request)
        {
            Console.WriteLine($"   ✅ {_title}批准了请假申请");
            Console.WriteLine($"   批准天数: {request.Days}天");
            Console.WriteLine($"   批准时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }
    }

    // 具体处理者 - 部门经理
    public class DepartmentManagerLeave : LeaveApprover
    {
        public DepartmentManagerLeave() : base("部门经理", 5) { }

        protected override void ApproveLeave(LeaveRequest request)
        {
            Console.WriteLine($"   ✅ {_title}批准了请假申请");
            Console.WriteLine($"   批准天数: {request.Days}天");
            Console.WriteLine($"   批准时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            
            if (request.Days > 3)
            {
                Console.WriteLine($"   📝 备注: 请安排好工作交接");
            }
        }
    }

    // 具体处理者 - 人力资源总监
    public class HRDirector : LeaveApprover
    {
        public HRDirector() : base("人力资源总监", 15) { }

        protected override void ApproveLeave(LeaveRequest request)
        {
            Console.WriteLine($"   ✅ {_title}批准了请假申请");
            Console.WriteLine($"   批准天数: {request.Days}天");
            Console.WriteLine($"   批准时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"   📝 备注: 已记录到考勤系统");
            Console.WriteLine($"   📝 备注: 请假期间工资按规定发放");
        }

        protected override bool CanApprove(LeaveRequest request)
        {
            // HR需要检查请假记录
            Console.WriteLine($"   🔍 检查年度请假记录...");
            Thread.Sleep(300);
            Console.WriteLine($"   ✓ 剩余年假充足");
            return base.CanApprove(request);
        }
    }
}
