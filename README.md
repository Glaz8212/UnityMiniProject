# UnityMiniProject
 유니티 미니 프로젝트
---

2024.09.25 ~ 10.03

유니티 학습 내용을 활용해 미니 게임 컨텐츠 구현

---

## 개요

유니티에서 배운 내용들을 사용해서, 미니 게임을 만들어보는 프로젝트 진행.

2D, 3D 장르는 자유이며 기획에 시간을 쓰지 않기 위해, 모작으로 진행한다.

### 모작 대상 : Portal


3D를 사용해서 만들고 싶고, 

유니티의 기능들을 학습하면서 가장 흥미로웠던 ‘레이캐스트’를 잘 활용할 수 있는
FPS장르를 만들어 보고 싶었다.

그렇지만, 과제에서 이미 구현해보았던 단순한 총알 발사, 효과음, 파티클, 피격등을 구현하는
정통 FPS가 아닌 다른 기능까지 활용해 볼 수 있을 것 같은 ‘포탈’을 모작한다.

### Portal의 주요 기능

[포탈 티저] https://www.youtube.com/watch?v=TluRVBhmf8w

1. 총기 업그레이드 이전
    1. 좌 클릭시 이미 존재하는 노란색 포탈로 이동 가능 한 파란 포탈 생성.
    2. 포탈에는 반대 포탈의 모습이 렌더링 됨.
    3. 기믹을 위해서 본인 뿐만 아니라 발판 작동에 필요한 육면체 등을 이동 가능.
    
2. 총기 업그레이드 이후
    1. 좌 클릭 시 파란 포탈, 우 클릭 시 파란 포탈 과 연동되는 노란 포탈 생성.
    2. 나머지는 위와 같음

※ 사실 상, 기믹은 저 정도가 전부이고 추가적으로

 +) 포탈로 반복해서 떨어지면서 가속도를 이용해 스테이지를 클리어하는 기믹

정도를 시간이 남으면 구현해보면 될 것 같다.

반대 포탈의 모습이 랜더링 되는 그래픽적 요소가 핵심이면서, 가장 구현하기 어려울 것 같다.

프로젝트 시작하기 전 조사해본 바로는, URP 렌더러의 Stencil 기능을 활용하면 될 것 같다.

### 개발 예정 순서

- 프로토 타입 개발 ( 핵심 기능 )
    - 캐릭터 이동 ★★★
        - FPS 시점과, 점프, 이동 구현
    - 포탈건 사용 (UI) ★★
        - 화면에 포탈건 상태 ( 쿨타임 등 ) 표시
        - 좌클릭 ( 파란 포탈 ) , 우클릭 ( 노란 포탈 )
    - 포탈 생성 및 이동 기능 ★★★★★
        - 포탈 간 연결 및 이동 구현 ( 플레이어 & 오브젝트 )
        - 포탈 시각화 ( 반대 포탈 화면 )
- 컨텐츠 ( 추가 기능 )
    - 스테이지 생성 ★★★
        - 기본적인 퍼즐 및 구조 구현
    - UI 개선 및 재시작 기능 ★★
- 퀄리티
    - 버그 수정 및 다듬기 ★★★
    - 디테일 추가 ★★
        - 사운드 및 파티클 등.

---

## 핵심기능 :  URP Stencil Buffer

※ 참고 영상

[Making Portals with Shader Graph in Unity! (Tutorial)](https://www.youtube.com/watch?v=TkzASwVgnj8)

※ 참고 페이지

[Portals | Part 2 - Stencil-based Portals](https://danielilett.com/2019-12-14-tut4-2-portal-rendering/)

구현 단계

※ 1. Inner Cube에 Render Texture 출력 과정.

두 개의 렌더 텍스쳐를 만들고 A,B의 포탈에 카메라 컴포넌트를 추가한다.
각각의 카메라가 촬영하는 화면을 상대 렌더 텍스쳐에 추가 후,
INNER CUBE 에서 출력한다.

※ 2. 

2개의 (unlit) shader를 만들고,

```csharp
Shader "Custom/PortalStencil"
{
    SubShader
    {
        Tags { "Queue" = "Geometry" }

        Pass
        {
            // 색상 출력 x 스텐실 값만 설정
            ColorMask 0

            Stencil
            {
                Ref 1
                Comp Always   // 항상 기록
                Pass Replace  // 통과할 때 스텐실 값 1
                
            }
        }
    }
}
```

<포탈에 적용할 스텐실>

통과할 때 스텐실 값을 1로 기록한다.

```csharp
Shader "Custom/ObjectStencil"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue" = "Geometry" }

        Pass
        {
            // Stencil 설정: 스텐실 값이 1과 같을 때만 렌더링
            Stencil
            {
                Ref 1          // 스텐실 참조 값
                Comp Equal     // 스텐실 값이 1일 때만 렌더링
                Pass Keep      // 통과할 때 스텐실 값 유지
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}

```

<통과하는 오브젝트에 적용할 셰이더>

스텐실의 값이 1인 (즉, 포탈을 통과한) 부분을 렌더링한다.
