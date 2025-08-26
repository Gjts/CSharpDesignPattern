---
title: Requirements — 架构、技术与平台集成要求
date: 2025-08-05
author: Indie Dev
version: v1.0.0
---

## 系统架构（高层）

```mermaid
flowchart LR
U[Web/插件前端 (Next.js 14)] --> G[API 网关 (.NET 8 Minimal API)]
G --> A[业务服务: 简历/字幕/会议/本地化/电商]
G --> Auth[身份&计费: Clerk/自研 + Stripe/Paddle]
A --> DB[(PostgreSQL + Redis)]
A --> LLM[LLM 层: GPT-4-turbo + 规则引擎]
Obs[Observability: PostHog/Otel] --> G
```

## 技术栈与库

| 层 | 选型 |
| --- | --- |
| 前端 | Next.js 14（App Router）、Tailwind、Radix UI、Fluent UI（Word 插件端）|
| 后端 | .NET 8 Minimal API、EF Core、MediatR、Polly 重试 |
| 数据 | PostgreSQL、Redis（缓存/队列） |
| AI | OpenAI GPT-4-turbo、文本嵌入检索、语音转写（可替换） |
| 观测 | OpenTelemetry、PostHog、Serilog |
| 支付 | Stripe Billing + Paddle（冗余） |
| 部署 | Railway（API/DB）、Cloudflare Pages（Web/Docs） |

## 服务划分

- profile-service：用户/订阅/配额
- content-service：改写/字幕/本地化模板
- meeting-service：转写/要点/提案
- commerce-service：商品素材/SEO/合规
- integration-service：平台 OAuth/Webhook/回写

## 平台集成矩阵（功能优化版）

| 平台 | 接入方式 | 关键能力 | 审核周期 | 分成 | 文档 |
| --- | --- | --- | --- | ---: | --- |
| Microsoft Word | Office JavaScript API + Office Add-in | 任务窗格、命令、SSO | 3–5 天 | 0% | https://learn.microsoft.com/office/dev/add-ins/ |
| Notion | Notion API + OAuth 公共集成 | 读取/写回页面与数据库 | 0 天 | 0% | https://developers.notion.com/ |
| Zoom | Zoom Meeting SDK + App Marketplace | 会议内面板、事件订阅 | 3–7 天 | 0% | https://developers.zoom.us/docs/zoom-apps/ |
| Figma | Figma Plugin API | 读取/写回节点、导出 | 1–2 天 | 0% | https://www.figma.com/plugin-docs/intro/ |
| Shopify | GraphQL Admin API + Assets API | 读写产品/媒体/元字段 | 1–3 天 | 0% | https://shopify.dev/docs/api/admin-graphql |

## API 设计（示例）

| 方法 | 路径 | 用途 |
| --- | --- | --- |
| POST | /api/resume/score | JD × 简历评分与建议 |
| POST | /api/video/subtitles | 生成字幕/章节点 |
| POST | /api/meet/summary | 会议要点与提案草稿 |
| POST | /api/figma/l10n | 本地化检测与建议 |
| POST | /api/shopify/optimize | 图文合规与 SEO Alt |

## 数据模型（示例）

- users(id, email, plan, quota, created_at)
- projects(id, user_id, type, meta, created_at)
- events(id, user_id, name, props, ts)
- subscriptions(id, user_id, provider, status, renew_at)

## 安全与合规

- OAuth 最小权限；离线 token 加密（KMS）
- PII 脱敏与按地区存储策略；提供导出/删除自助
- 速率限制：用户/分钟、IP/分钟、端点配额

## 部署与环境

- Railway：api（2 实例）、worker、pg、redis；生产/预发分离
- Cloudflare Pages：web、docs、状态页；KV 用于配置/特性开关
- 灰度策略：按用户分群/地区/平台

## 监控指标

- 应用：95/99 延迟、错误率、队列堆积
- 业务：激活率、付费转化、留存、单位成本（$/结果）
- 平台：API 失败率、审核状态、Webhook 重试率