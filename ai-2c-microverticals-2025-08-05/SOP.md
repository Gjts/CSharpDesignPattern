---
title: SOP — 研发、上架、运营标准作业流程
date: 2025-08-05
author: Indie Dev
version: v1.0.0
---

## 通用研发 SOP（Next.js 14 + .NET 8 + PostgreSQL）

- [ ] 创建单仓 monorepo（apps/web、apps/api、packages/shared）
- [ ] 统一环境变量管理（.env.template → Doppler/1Password）
- [ ] AuthN：邮箱魔法链接 + OAuth（Notion/Zoom/Shopify/Word/Figma）
- [ ] Stripe/Paddle 订阅（试用/优惠码/退款）
- [ ] Observability：PostHog + OpenTelemetry + Health endpoints
- [ ] 审计日志与删除请求（GDPR/CCPA）

## 模型与提示词 SOP

- [ ] 分层调用：规则/模板 → 轻模型；创作/改写 → GPT-4-turbo
- [ ] 结果可验证：评分器/对齐指标（如可读性、覆盖度、冗余）
- [ ] 缓存：语义缓存 + 去重；阈值外再调用
- [ ] 成本护栏：单用户/日用量上限 + 退避重试

## 平台上架 SOP（功能优化版）

### Word（JDMatch for Word）

- [ ] Office JS 清单/任务窗格 UI（Fluent UI）
- [ ] 单点登录与同域校验；提交至 Microsoft AppSource
- [ ] 审核要点：权限最小化、无宏执行、清晰的隐私条款

### Notion（ClipPlan AI）

- [ ] OAuth 公共集成；申请 Notion 官方公开集成
- [ ] 权限仅限读取/写回必要数据库表
- [ ] 提供删除数据 API 与自助页面

### Zoom（ZoomCoach）

- [ ] Meeting SDK 与 Events 订阅；App 内浮层遵循 Zoom 设计
- [ ] 审核材料：视频演示、权限说明、日志范围
- [ ] GDPR：会议音频转写仅短期缓存，默认关闭录制

### Figma（L10n Check AI）

- [ ] 插件面板/快捷键/暗色模式适配
- [ ] 权限：只访问当前文件与选择的节点
- [ ] 审核材料：动图/最小权限/崩溃恢复

### Shopify（ListingLens）

- [ ] 使用 Polaris UI；嵌入式导航
- [ ] GraphQL Admin API（Products/Metafields/Files/Assets）
- [ ] 审核材料：权限说明、支持邮箱/隐私条款/计费截图

## 运营 SOP

- [ ] 上线当天发布：Product Hunt、Show HN、Twitter/X 线程、YouTube 教程
- [ ] 建立邮件序列：欢迎→价值案例→计费提醒→留存 tips（D1/D3/D7）
- [ ] 建立联盟计划：首月 20% 返利；提供追踪链接与素材包
- [ ] 客服：Intercom/HelpKit + FAQ（截图、短视频）

## 数据与隐私 SOP

- [ ] 数据最小化：默认不存原文；敏感内容区域屏蔽（自定义）
- [ ] 用户数据导出与删除：自助面板 + webhook 触发作业
- [ ] 安全：API Key 加密、行级权限、速率限制

## 故障应急 SOP

- [ ] 模型/第三方故障自动降级（缓存/队列重试/备用模型）
- [ ] 灰度开关：按产品/地区/配额下发开关
- [ ] 状态页 + 事后复盘模板（影响、根因、行动项）

## 交付物与验收清单

- [ ] 五个 MVP 可用，完成平台上架
- [ ] 支付闭环与退款流程
- [ ] 埋点可见且有基础仪表板
- [ ] 法务页面（ToS/Privacy/数据处理）与 DPA 模板