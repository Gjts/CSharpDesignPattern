# C# 设计模式项目结构说明

## 📁 项目目录结构规范

本项目采用统一的目录结构来组织所有设计模式的实现，每个设计模式都遵循相同的组织方式。

### 🏗️ 标准目录结构

```
Src/
├── [XX]PatternName/                 # 设计模式根目录
│   ├── 01ImplementationMethod/      # 基础实现方法
│   │   ├── 01BasicImplementation.cs # 基础实现1
│   │   ├── 02AdvancedImpl.cs       # 进阶实现2
│   │   └── ...                      # 其他实现变体
│   ├── 02Example/                   # 实际业务案例
│   │   ├── RealWorldExample1/       # 实际案例1
│   │   │   ├── Program.cs          # 案例入口
│   │   │   └── Implementation.cs    # 案例实现
│   │   └── RealWorldExample2/       # 实际案例2
│   │       └── ...
│   ├── Program.cs                   # 主程序入口
│   └── [XX]PatternName.csproj      # 项目文件
```

### 📋 目录说明

#### 01ImplementationMethod/
- **用途**: 存放设计模式的基础实现和变体
- **内容**: 
  - 设计模式的标准实现
  - 不同的实现方式和变体
  - 基础示例代码
- **命名规范**: 文件按序号命名，如 `01BasicImpl.cs`, `02AdvancedImpl.cs`

#### 02Example/
- **用途**: 存放实际业务场景的应用案例
- **内容**:
  - 真实项目中的应用示例
  - 行业特定的解决方案
  - 复杂的业务场景实现
- **组织方式**: 每个案例一个子目录，包含独立的实现

#### Program.cs
- **用途**: 设计模式的主入口程序
- **功能**:
  - 展示设计模式的核心概念
  - 调用示例代码演示
  - 提供运行入口

## 🎯 实际业务案例分类

### 🏭 WMS（仓库管理系统）相关
- **Interpreter Pattern**: WMS规则引擎和查询语言
- **Command Pattern**: 仓库操作命令（入库、出库、移库、盘点）

### 🤖 AI/机器学习相关
- **Template Method Pattern**: AI模型训练流程（GPT、CNN、推荐系统）
- **Strategy Pattern**: AI推荐算法策略
- **Memento Pattern**: AI模型检查点保存

### 🔗 Web3/区块链相关
- **Chain of Responsibility Pattern**: Web3交易验证链
- **Iterator Pattern**: 区块链和交易遍历
- **State Pattern**: NFT生命周期状态
- **Visitor Pattern**: 智能合约审计

### 🌐 互联网/微服务相关
- **Mediator Pattern**: 微服务协调器
- **Observer Pattern**: 实时监控和通知系统

## 📊 设计模式完成状态

### ✅ 创建型模式 (6/6)
- ✅ Singleton（单例模式）
- ✅ Simple Factory（简单工厂模式）
- ✅ Factory Method（工厂方法模式）
- ✅ Abstract Factory（抽象工厂模式）
- ✅ Builder（建造者模式）
- ✅ Prototype（原型模式）

### ✅ 结构型模式 (7/7)
- ✅ Adapter（适配器模式）
- ✅ Bridge（桥接模式）
- ✅ Composite（组合模式）
- ✅ Decorator（装饰器模式）
- ✅ Facade（外观模式）
- ✅ Flyweight（享元模式）
- ✅ Proxy（代理模式）

### ✅ 行为型模式 (11/11)
- ✅ Interpreter（解释器模式）- WMS规则引擎
- ✅ Template Method（模板方法模式）- AI模型训练
- ✅ Chain of Responsibility（责任链模式）- Web3交易验证
- ✅ Command（命令模式）- WMS操作命令
- ✅ Iterator（迭代器模式）- 区块链遍历
- ✅ Mediator（中介者模式）- 微服务协调
- ✅ Memento（备忘录模式）- AI模型检查点
- ✅ Observer（观察者模式）- 实时监控
- ✅ State（状态模式）- NFT生命周期
- ✅ Strategy（策略模式）- AI推荐算法
- ✅ Visitor（访问者模式）- 智能合约审计

## 🚀 如何使用

### 运行特定设计模式
```bash
cd Src/[XX]PatternName
dotnet run
```

### 查看基础实现
导航到 `01ImplementationMethod/` 目录查看设计模式的各种实现方式。

### 查看实际案例
导航到 `02Example/` 目录查看真实业务场景的应用。

## 💡 最佳实践

1. **代码组织**: 保持代码结构清晰，每个类一个文件
2. **命名规范**: 使用描述性的名称，遵循C#命名约定
3. **文档注释**: 为复杂逻辑添加XML文档注释
4. **示例完整**: 确保每个示例都是可运行的完整代码
5. **业务相关**: 优先使用真实业务场景作为示例

## 📚 学习建议

1. **基础学习路径**:
   - 先阅读 `01ImplementationMethod/` 理解模式原理
   - 再查看 `02Example/` 了解实际应用

2. **进阶学习路径**:
   - 对比不同模式的实现差异
   - 分析实际案例中的设计决策
   - 尝试将模式应用到自己的项目中

3. **实践建议**:
   - 修改示例代码，观察行为变化
   - 创建自己的实际案例
   - 组合多个设计模式解决复杂问题

## 🔄 项目维护

- **版本**: .NET 8.0 + C# 12
- **更新时间**: 2024-08
- **维护者**: Gjts
- **许可证**: MIT

---

**注意**: 本项目持续更新中，欢迎贡献更多实际业务案例！