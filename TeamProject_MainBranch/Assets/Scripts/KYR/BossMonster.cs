using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    public enum Element //보스 몬스터들의 원소 타입을 정의한다.
    {
        Ice, //0: 얼음
        Rock, //1: 돌
        Lava, //2: 용암
        Dia, //3. 다이아몬드
        Water,//4. 물
        Metal//5. 강철
    }

    public enum AnimationType
    {
        Attack,//0: 투척 or 때리기
        AttackN,
        Rotate
    }
    public CoolTimeHndl skillHndl;

    /// <summary>
    /// Boss Information
    /// </summary>
    public Element m_type;
    public GameObject m_bossHand;
    public GameObject m_throughObjectInHand; //투척 오브젝트 prefab
    public GameObject m_throughObjectPrefab; //투척 오브젝트 prefab

    //회전 회오리 이펙트 Gameobject
    public GameObject m_EffectTornado;
    //쉴드 오브젝트 프리팹
    public GameObject m_shieldObject;
    //공기 팡 damageGuid
    public GameObject m_bombguidPrefab;
    public GameObject m_EffectBombParticle;

    private List<BossSkill> m_AdvancedSkills;//보스가 가진 특수 패턴

    private BossSkill m_SpecialSkill; //원소에 따른 특수 패턴 1가지.

    private Animator m_Animator;
    public delegate void m_defaultFeature(); //원소 특성에 따른 디폴트 특징. (변경될 수 있음)


    private bool m_isChasing;

    [SerializeField]
    private GameObject[] m_damageGuidGraphic = new GameObject[2];//2개.[0] sub, [1] base
    private GameObject throws;



    // Start is called before the first frame update
    void Start()
    {
        m_Animator = this.transform.GetComponent<Animator>();
        m_isChasing = true;
        m_throughObjectInHand.SetActive(false);
        setBossSkillSets(m_type);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isChasing)
            Normal_Chasing();
    }

    private void setBossSkillSets(Element element)
    {
        initBossNormalSkills();
        initBossAdvanceSkills(element);
    }

    private void initBossNormalSkills()
    {
        skillHndl.m_NormalSkills = new List<BossSkill>();
        skillHndl.m_NormalSkills.Add(new BossSkill(10, 15, Special_02Bombing));
        skillHndl.m_NormalSkills.Add(new BossSkill(10, 10, Special_BossDefence));
        skillHndl.m_NormalSkills.Add(new BossSkill(10, 8, Special_CreateTornado));
        skillHndl.m_NormalSkills.Add(new BossSkill(10, 5, Normal_Throwing));
    }

    private void initBossAdvanceSkills(Element element)
    {
        switch(element)
        {
            case Element.Ice:
                break;
            case Element.Metal:
                break;
            case Element.Rock:
                break;
            case Element.Water:
                break;
            case Element.Lava:
                break;
            case Element.Dia:
                break;
        }
    }

    /// <summary>
    /// 투척: 자신의 등 뒤에 있는 오브젝트 1개를 주인공에게 던진다. 던져진 오브젝트는 3초 뒤에 사라지며 원래 위치로 리스폰 된다.데미지는 5이다
    /// </summary>
    private void Normal_Throwing()
    {
        m_isChasing = false;

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(player);
        if (Vector3.Distance(transform.position, player.position) >= 1)
        {
            m_throughObjectInHand.SetActive(true);

        }

        m_Animator.SetInteger("AttackType", (int)AnimationType.Attack);
        m_Animator.SetTrigger("IsAttack");

    }

    //Animation Event 함수
    private void AE_throwObject()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(player);
        m_throughObjectInHand.SetActive(false);
        if (Vector3.Distance(transform.position, player.position) >= 1)
        {  
            StartCoroutine(throwing());
        }
    }

    IEnumerator throwing()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 throwsPos = new Vector3(player.position.x, player.position.y + 5.0f, player.position.z);

        throws = Instantiate(m_throughObjectPrefab, throwsPos, Quaternion.Euler(0, 0, 0));
        throws.transform.localScale = new Vector3(10, 10, 10);
        throws.transform.GetComponent<Rigidbody>().useGravity = false;
        throws.transform.position = new Vector3(player.position.x, player.position.y + 5.0f, player.position.z);

        m_damageGuidGraphic[0] = throws.transform.GetChild(0).gameObject;//sub
        m_damageGuidGraphic[1] = throws.transform.GetChild(1).gameObject;//base

        while (m_damageGuidGraphic[0].transform.position.y <= m_damageGuidGraphic[1].transform.position.y)
        {
            float upadate = m_damageGuidGraphic[0].transform.position.y + 0.05f;
            m_damageGuidGraphic[0].transform.position =
                new Vector3(m_damageGuidGraphic[0].transform.position.x, upadate, m_damageGuidGraphic[0].transform.position.z);
            yield return new WaitForEndOfFrame();
        }


        throws.transform.GetComponent<Rigidbody>().useGravity = true;

        Destroy(throws, 5.0f);
   
        //투척하는 모션에서 오브젝트가 손에서 사라진 즉시 함수가 발동 됨.
        yield return null;
    }

    /// <summary>
    /// 아무것도 하지 않고, 플레이어 방향으로 다가간다.
    /// </summary>
    private void Normal_Chasing()
    {
        int MoveSpeed = 4;
        int MinDist = 20;

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(player);

        if (Vector3.Distance(transform.position, player.position) >= MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
    }


    /// <summary>
    /// 원소 정령이 360도 회전을 반복하여 주인공을 향해 10초간 돌진한다. 해당
    ///공격이 끝나고 나면 원소 정령은 2초간 상태 이상으로 아무것도 할 수 없다.(어지러워서 잠시 서 있다는 컨셉이다.) 
    ///데미지는 초당 3이다.접촉되어 있는 만큼 데미지가 들어간다.
    /// </summary>
    private void Normal_Rotating()
    {
        m_Animator.SetInteger("AttackType", (int)AnimationType.Rotate);
        m_Animator.SetTrigger("IsAttack");
        m_isChasing = false;
        StartCoroutine(moveToward());
    }

    IEnumerator moveToward()
    {
        while (m_Animator.GetInteger("AttackType") == (int)AnimationType.Rotate)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            int MinDist = 5;
            int MoveSpeed = 4;
            if (Vector3.Distance(transform.position, player.position) >= MinDist)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, MoveSpeed * Time.deltaTime);

            }
            yield return new WaitForEndOfFrame();
        }
        m_isChasing = true;
        yield return null;
    }


    /// <summary>
    /// 회전 회오리 소환: 맵의 랜덤한 위치에 랜덤한 개수의 회전하는 회오리를 생성한다.
    ///회오리는 10초간 유지 된다.이 회오리는 일정 범위 내에 주인공이나 동반자가 들어오면 회오리의 중심으로 빨아들인다.
    /// 주인공은 달리기를 통해 빠져나갈 수 있다.
    ///데미지는 초당 2이며, 접촉되어 있는 만큼 데미지가 들어간다
    /// </summary>
    private void Special_CreateTornado()
    {
        //동시 생성 회오리 개수 랜덤 3~5
        int TordateCount = Random.Range(5, 10);
        Debug.Log("토네이도 생성" + TordateCount);
        //동시 생성 회오리 중복X rotation 랜덤 0~360'
        for (int i = 0; i < TordateCount; i++)
        {
            //랜덤 방향에 회오리 생성
            Vector3 direction = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * Vector3.forward;//벡터 정규화

            StartCoroutine(spawnTornadoRnd(direction));
        }
        //IEmuerator 회오리 이펙트 끝날때까지 해당 방향으로 이동.
    }

    IEnumerator spawnTornadoRnd(Vector3 direction)
    {
        GameObject spawned = Instantiate(m_EffectTornado, transform.position + direction * 5.0f, m_EffectTornado.transform.rotation);
        StartCoroutine(ParticalDestroyCounter(spawned, 4.0f));
        while (spawned != null)
        {

            spawned.transform.position += direction * Time.deltaTime * 5.0f;

            yield return new WaitForEndOfFrame();//안하면 과부화 걸림!
        }


        yield return null;
    }

    IEnumerator ParticalDestroyCounter(GameObject spawned, float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            Destroy(spawned);
            spawned = null;

            break;
        }
        yield return null;
    }

    /// <summary>
    /// 반격의 방패: 원소 정령이 자신의 몸 전체를 덮는 방어막을 두른다. 
    /// 이 상태일 떄 공격하면 모든 공격을 튕겨내어, 공격을 한 주체가 피해를 입는다.
    /// </summary>
    private void Special_BossDefence()
    {
        Vector3 instancePos = new Vector3(m_shieldObject.transform.position.x, m_shieldObject.transform.position.y, m_shieldObject.transform.position.z);
        GameObject spawned = Instantiate(m_shieldObject, instancePos, m_shieldObject.transform.rotation);
        StartCoroutine(ParticalDestroyCounter(spawned, 4.0f));
    }

    private void Special_02Bombing()
    {
        //Projection Prefab Instantiate
        /*
          public GameObject m_bombguidPrefab;
          public GameObject m_EffectBombParticle;
         */
        Vector3 pos = new Vector3(transform.position.x, m_bombguidPrefab.transform.position.y, transform.position.z);
        Vector3 dir = ( GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        GameObject guid = Instantiate(m_bombguidPrefab, pos, Quaternion.LookRotation(dir));

        StartCoroutine(bombDelay(guid, dir));
    }

    IEnumerator bombDelay(GameObject guid, Vector3 dir)
    {

        yield return StartCoroutine(skillRangeGraphicAnim(guid));
        Destroy(guid);
        float dis = 2.0f;
        //EffectPrefab Instantiate
        for(int i = 2; i<7; i+=2)
        {
            calculateBombQuaternion(dir, i, dis+=5.0f);
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }

    IEnumerator skillRangeGraphicAnim(GameObject guid)
    {
        m_damageGuidGraphic[0] = guid.transform.GetChild(0).gameObject;//sub
        m_damageGuidGraphic[1] = guid.transform.GetChild(1).gameObject;//base

        while (m_damageGuidGraphic[0].transform.position.y >= m_damageGuidGraphic[1].transform.position.y)
        {
            float upadate = m_damageGuidGraphic[1].transform.position.y + 0.05f;
            m_damageGuidGraphic[1].transform.position =
                new Vector3(m_damageGuidGraphic[0].transform.position.x, upadate, m_damageGuidGraphic[0].transform.position.z);
            yield return new WaitForSeconds(0.005f);
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    private void calculateBombQuaternion(Vector3 dir, int effectCount, float dis)
    {

        int[] rot = new int[7] { 0, -15, 15, -30, 30, -45, 45 };
        for (int i = 0; i < effectCount; i++)
        {
            Vector3 direction = Quaternion.AngleAxis(rot[i], Vector3.up) * dir;//벡터 정규화
            GameObject effect = Instantiate(m_EffectBombParticle, transform.position + direction * dis, Quaternion.LookRotation(direction));
            StartCoroutine(ParticalDestroyCounter(effect, 4.0f));//s 뒤 destroy
        }
    }



}
