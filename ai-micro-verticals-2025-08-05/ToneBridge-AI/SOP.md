---
title: ToneBridge AI — 研发与运营 SOP
date: 2025-08-05
author: 独立开发者
version: v1.0.0
---

## 研发流程

- 单周迭代：需求→开发→验收→上架；每周 1 次小版本。
- 语气词典由 YAML 管理，版本化与回滚。

## 部署

| 环节 | 工具 | 说明 |
|---|---|---|
| 前端 | Cloudflare Pages | 主干自动部署 |
| 后端 | Railway | 主干自动构建 |
| DB | PostgreSQL | EF Core 迁移 |

## 监控

- 指标：改写成功率、响应时延、试用→付费、留存。
- 工具：事件表 + PostHog。

## 支付与合规

- Stripe 主、Paddle 备；2 天退款窗口；敏感内容过滤规则。

## 插件上架 SOP

- Gmail Add-on：Apps Script 清单 → 卡片 UI → Workspace Marketplace 提交（描述、权限说明、隐私政策）。
- Outlook Add-in：Office.js 清单 → 侧栏与上下文命令 → AppSource 提交。

## 客服

- 邮件与站内反馈；48 小时内响应；负反馈入产品看板。

