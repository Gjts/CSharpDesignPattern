---
title: Requirements – 技术与合规
date: 2025-08-05
author: IndieDev
version: 1.0
---

## 共用技术栈

| 层 | 技术 |
|----|------|
| 前端 | Next.js 14, Tailwind, shadcn/ui |
| 后端 | .NET 8 Minimal API, EF Core |
| 数据库 | PostgreSQL (Supabase) |
| AI | OpenAI GPT-4o + 本地 fine-tune 模型 |
| DevOps | Railway CI, Cloudflare Pages |
| 监控 | Sentry, Grafana Cloud |
| 支付 | Stripe + Paddle |

## 功能优化版接入信息

| 插件 | Platform API | 审核周期 | 分成 |
|------|-------------|---------|------|
| PatchGuard AI | GitHub Checks API + GitHub Apps | 3–5 天 | 0% |
| NutriLabel AI | Shopify GraphQL Admin API | 1–3 天 | 0% |
| AccentCoach AI | Zoom Apps SDK + Webhooks | 2–4 天 | 0% |
| ChargeCheck AI | Stripe Apps Framework | 1–2 天 | 0% |
| PrivacyShield AI | Canva Apps SDK | 2–3 天 | 0% |

## 安全 & 合规

- GDPR / CCPA 同意管理  
- 数据加密：AES-256 at rest，TLS 1.3 in transit  
- SOC2 Type I 计划：2026 Q1