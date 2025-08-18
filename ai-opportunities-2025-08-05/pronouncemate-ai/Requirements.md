---
title: PronounceMate AI — 需求规格说明
date: 2025-08-05
author: 独立开发者
version: 1.0
---

## 架构

```mermaid
flowchart LR
  U[Web/PWA] -->|录音/上传| G[Next.js 14]
  G --> A[.NET 8 Minimal API]
  A --> Q[(PostgreSQL)]
  A --> O[对象存储(音频片段)]
  A --> L[OpenAI GPT-4-turbo]
  A --> Z[Zoom App SDK]
  G --> P[Stripe/Paddle]
```

## 接口定义（示例）

| 方法 | 路径 | 描述 | 入参 | 出参 |
| --- | --- | --- | --- | --- |
| POST | /api/speech/score | 上传音频并评分 | file, targetAccent | {score, mistakes[]} |
| GET | /api/plan | 获取 30 天训练计划 | auth | {days[]} |
| POST | /api/report | 生成会议后报告 | recordingId | {summary, topMistakes[]} |

## 数据库（核心表）

| 表 | 关键字段 | 说明 |
| --- | --- | --- |
| users | id, email, locale, plan | 用户与订阅 |
| recordings | id, userId, uri, duration | 音频记录 |
| scores | id, recordingId, phoneme, score | 评分明细 |
| subscriptions | id, userId, provider, status | 订阅状态 |

## 非功能

- 性能：评分 P95 ≤ 2.5s；可用性 ≥ 99.5%。
- 隐私：GDPR/CCPA；数据最小化；加密 at-rest/in-transit。
- 监控：APM、错误率、支付成功率、退订率。