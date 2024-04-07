# CSharpDesignPattern
## 设计模式是什么？

**设计模式是软件设计中常见问题解决方案。它们是根据需求制定不同的优化方案来优化项目的整体结构，可用于解决代码中反复出现的设计问题。**

## 设计模式分为哪几种？

    1. 创建型设计模式：使用不同的创建方式来创建对象，关注点在将对象的创建与使用分离。

    2. 结构型设计模式：如何将对象和类组装成更大的结构，并同时保持结构的灵活和高效。

    3. 行为型设计模式：多个类或对象之间的相互协作。

### 设计模式六大原则:

    1. 单一职责原则：一个类应该只有一个发生变化的原因。

    2. 开闭原则：对扩展开放，对修改关闭（使用接口或者抽象类，让代码支持可配置）。

    3. 里氏替换原则：有父类的地方就可以用子类来代替。

    4. 迪米特法则：如果两个程序无须直接通信，那么就不应当发生直接的相互调用，通过第三方服务来打通之间的数据，降低类之间的耦合度。

    5. 接口隔离原则：一个类对另一个类的依赖应该建立在最小的接口上。

    6. 依赖倒置原则：上层模块不应该依赖底层模块，它们都应该依赖于抽象（面向接口，面向服务）

## 创建型设计模式
1. [Singleton（单例模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/01Singleton)

    使用场景： 当需要确保一个类只有一个实例，并提供一个全局访问点时。

    总结： 确保一个类只有一个实例并提供全局访问。
		

2. [Simple Factory（简单工厂模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/02SimpleFactory)

    使用场景： 在创建对象时，不会对客户端暴露创建逻辑，并且是通过使用一个共同的接口来指向新创建的对象。

    总结： 通过一个接口创建对象，隐藏创建逻辑。
	

3. [Factory Method（工厂方法模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/03FactoryMethod)

    使用场景： 当一个类需要其子类来指定创建的对象时。

    总结： 子类决定实例化哪一个类。

4. [Abstract Factory（抽象工厂模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/04AbstractFactory)

    使用场景： 当需要创建一系列相关或相互依赖的对象时，且不需要指定它们具体的类。

    总结： 创建相关的对象家族而不需指定具体类。

5. [Builder（建造者模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/05Builder)

    使用场景： 当创建复杂对象的算法应该独立于该对象的组成部分以及它们的装配方式时。

    总结： 分步骤构建一个复杂的对象。

6. [Prototype（原型模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/06Prototype)

    使用场景： 当需要复制一个对象，同时又希望不增加与对象相关的代码时。
    
    总结： 通过复制现有的实例来创建新的实例。
 
## 结构型设计模式
7. [Adapter（适配器模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/07Adapter)

    使用场景： 当希望将一个类的接口转换成客户端期望的另一个接口时，使得原本接口不兼容的类可以一起工作。
	
    总结： 允许不兼容的对象能够合作。

8. [Bridge（桥接模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/08Bridge)

    使用场景： 当希望将抽象部分与它的实现部分分离，使它们都可以独立地变化时。
	
    总结： 分离抽象与实现，使它们可以独立变化。

9. [Composite（组合模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/09Compsite)

    使用场景： 当需要将对象组合成树形结构以表示部分-整体的层次结构时，使得用户对单个对象和组合对象的使用具有一致性。
	
    总结： 以树形结构组合对象，表示部分整体层次。

10. [Decorator（装饰器模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/10Decorator)

    使用场景： 当希望动态地给一个对象添加一些额外的职责时，而不是通过继承一个子类。
	
    总结： 动态地给对象添加额外的职责。

11. [Facade（外观模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/11Facade)

    使用场景： 当需要为一个复杂的子系统提供一个简单的接口时。
	
    总结： 提供一个统一的接口以访问子系统中的一群接口。

12. [Flyweight（享元模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/12Flyweight)

    使用场景： 当需要大量的对象而且对象之间有大量的重复状态时，可以通过共享来减少资源消耗。
	
    总结： 通过共享来支持大量细粒度的对象。

13. [Proxy（代理模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/13Proxy)

    使用场景： 当需要为另一个对象提供一个代理或占位符以控制对这个对象的访问时。
	
    总结： 提供一个对象的代理以控制对它的访问。

## 行为型设计模式

14. [Interpreter（解释器模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/14Interpreter)

    使用场景： 当有一个语言需要解释执行，并且可以将该语言中的句子表示为一个抽象语法树时。
	
    总结： 为语言创建解释器，通常由抽象语法树表示。

15. [Template Method（模板方法模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/15Template)

    使用场景： 当在一个方法中定义一个算法的骨架，而将一些步骤延迟到子类中实现时。
	
    总结： 在一个方法中定义算法的骨架，延迟步骤到子类。

16. [Chain of Responsibility（责任链模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/16Respon)

    使用场景： 当需要多个对象中的一个来处理请求时，将这些对象连成一条链，并沿着这条链传递该请求，直到有一个对象处理它为止。
	
    总结： 通过链上的对象传递请求，直到被处理。

17. [Command（命令模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/17Command)

    使用场景： 当需要将请求封装为一个对象，以便使用不同的请求、队列或日志来参数化其他对象时，同时支持撤销操作。
	
    总结： 将请求封装成对象，以参数化客户端。

18. [Iterator（迭代器模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/18Iterator)

    使用场景： 当需要提供一种方法顺序访问一个聚合对象中的各个元素，而又不暴露其内部的表示时。
	
    总结： 顺序访问聚合对象中的元素，不暴露内部表示。

19. [Mediator（中介者模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/19Mediator)

    使用场景： 当一组对象以定义良好但复杂的方式进行通信时，通过一个中介对象来封装这些交互，简化了对象之间的通信。
	
    总结： 通过中介者封装一组对象的交互。

20. [Memento（备忘录模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/20Memento)

    使用场景： 当需要保存一个对象的状态以便在将来某个时刻可以恢复到这个状态时。
	
    总结： 保存对象的状态，以便恢复。

21. [Observer（观察者模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/21Observer)

    使用场景： 当一个对象的改变需要同时改变其他对象，而不知道具体有多少对象有待改变时。
	
    总结： 对象间的一对多依赖关系，当一个对象改变时，其依赖者得到通知并自动更新。

22. [State（状态模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/22State)

    使用场景： 当一个对象的行为取决于它的状态，并且它必须在运行时根据状态改变它的行为时。
	
    总结： 对象的行为依赖于其状态，状态改变时行为也随之改变。

23. [Strategy（策略模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/23Strategy)

    使用场景： 当一个类定义了多种行为，并且这些行为在这个类的操作中以多个条件语句的形式出现时，将这些行为封装在单独的策略类中，可以避免条件语句。
	
    总结： 定义一系列算法，使它们可以互换。

24. [Visitor（访问者模式）](https://github.com/Gjts/CSharpDesignPattern/tree/main/Src/24Visitor)

    使用场景： 当需要对一个对象结构中的对象进行很多不同且不相关的操作，而又不想修改这些对象的类时。
	
    总结： 在不改变元素类的前提下，增加作用于元素类上的新操作。

