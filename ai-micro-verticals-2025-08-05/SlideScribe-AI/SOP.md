---
title: SlideScribe AI — 研发与运营 SOP
date: 2025-08-05
author: 独立开发者
version: v1.0.0
---

## 研发

- 单周迭代；PR 检查清单（性能/隐私/可用性）。
- 主题与母版以 JSON Catalog 管理，版本化。

## 部署

| 环节 | 工具 | 说明 |
|---|---|---|
| 前端 | Cloudflare Pages | 自动化部署 |
| 后端 | Railway | 构建与回滚 |
| DB | PostgreSQL | 迁移与快照 |

## 监控

- KPI：生成时延、失败率、导出成功率、付费转化。
- 工具：事件表 + PostHog；异常告警。

## 支付与退款

- Stripe 主通道；2 天无条件退款；收集原因标签。

## 插件 SOP

- PowerPoint：Office.js 清单 → 侧栏与命令 → AppSource 提交。
- Google Slides：Apps Script 清单 → Workspace Marketplace 提交。

## 客服与知识库

- 站内工单 + 邮件；模板与案例知识库在线更新。

