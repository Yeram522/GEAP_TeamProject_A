using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    public enum Element //���� ���͵��� ���� Ÿ���� �����Ѵ�.
    {
        Ice, //0: ����
        Rock, //1: ��
        Lava, //2: ���
        Dia, //3. ���̾Ƹ��
        Water,//4. ��
        Metal//5. ��ö
    }

    public enum AnimationType
    {
        Attack,//0: ��ô or ������
        AttackN,
        Rotate
    }
    public CoolTimeHndl skillHndl;

    /// <summary>
    /// Boss Information
    /// </summary>
    public Element m_type;
    public GameObject m_bossHand;
    public GameObject m_throughObjectInHand; //��ô ������Ʈ prefab
    public GameObject m_throughObjectPrefab; //��ô ������Ʈ prefab

    //ȸ�� ȸ���� ����Ʈ Gameobject
    public GameObject m_EffectTornado;
    //���� ������Ʈ ������
    public GameObject m_shieldObject;
    //���� �� damageGuid
    public GameObject m_bombguidPrefab;
    public GameObject m_EffectBombParticle;

    private List<BossSkill> m_AdvancedSkills;//������ ���� Ư�� ����

    private BossSkill m_SpecialSkill; //���ҿ� ���� Ư�� ���� 1����.

    private Animator m_Animator;
    public delegate void m_defaultFeature(); //���� Ư���� ���� ����Ʈ Ư¡. (����� �� ����)


    private bool m_isChasing;

    [SerializeField]
    private GameObject[] m_damageGuidGraphic = new GameObject[2];//2��.[0] sub, [1] base
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
    /// ��ô: �ڽ��� �� �ڿ� �ִ� ������Ʈ 1���� ���ΰ����� ������. ������ ������Ʈ�� 3�� �ڿ� ������� ���� ��ġ�� ������ �ȴ�.�������� 5�̴�
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

    //Animation Event �Լ�
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
   
        //��ô�ϴ� ��ǿ��� ������Ʈ�� �տ��� ����� ��� �Լ��� �ߵ� ��.
        yield return null;
    }

    /// <summary>
    /// �ƹ��͵� ���� �ʰ�, �÷��̾� �������� �ٰ�����.
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
    /// ���� ������ 360�� ȸ���� �ݺ��Ͽ� ���ΰ��� ���� 10�ʰ� �����Ѵ�. �ش�
    ///������ ������ ���� ���� ������ 2�ʰ� ���� �̻����� �ƹ��͵� �� �� ����.(���������� ��� �� �ִٴ� �����̴�.) 
    ///�������� �ʴ� 3�̴�.���˵Ǿ� �ִ� ��ŭ �������� ����.
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
    /// ȸ�� ȸ���� ��ȯ: ���� ������ ��ġ�� ������ ������ ȸ���ϴ� ȸ������ �����Ѵ�.
    ///ȸ������ 10�ʰ� ���� �ȴ�.�� ȸ������ ���� ���� ���� ���ΰ��̳� �����ڰ� ������ ȸ������ �߽����� ���Ƶ��δ�.
    /// ���ΰ��� �޸��⸦ ���� �������� �� �ִ�.
    ///�������� �ʴ� 2�̸�, ���˵Ǿ� �ִ� ��ŭ �������� ����
    /// </summary>
    private void Special_CreateTornado()
    {
        //���� ���� ȸ���� ���� ���� 3~5
        int TordateCount = Random.Range(5, 10);
        Debug.Log("����̵� ����" + TordateCount);
        //���� ���� ȸ���� �ߺ�X rotation ���� 0~360'
        for (int i = 0; i < TordateCount; i++)
        {
            //���� ���⿡ ȸ���� ����
            Vector3 direction = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * Vector3.forward;//���� ����ȭ

            StartCoroutine(spawnTornadoRnd(direction));
        }
        //IEmuerator ȸ���� ����Ʈ ���������� �ش� �������� �̵�.
    }

    IEnumerator spawnTornadoRnd(Vector3 direction)
    {
        GameObject spawned = Instantiate(m_EffectTornado, transform.position + direction * 5.0f, m_EffectTornado.transform.rotation);
        StartCoroutine(ParticalDestroyCounter(spawned, 4.0f));
        while (spawned != null)
        {

            spawned.transform.position += direction * Time.deltaTime * 5.0f;

            yield return new WaitForEndOfFrame();//���ϸ� ����ȭ �ɸ�!
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
    /// �ݰ��� ����: ���� ������ �ڽ��� �� ��ü�� ���� ���� �θ���. 
    /// �� ������ �� �����ϸ� ��� ������ ƨ�ܳ���, ������ �� ��ü�� ���ظ� �Դ´�.
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
            Vector3 direction = Quaternion.AngleAxis(rot[i], Vector3.up) * dir;//���� ����ȭ
            GameObject effect = Instantiate(m_EffectBombParticle, transform.position + direction * dis, Quaternion.LookRotation(direction));
            StartCoroutine(ParticalDestroyCounter(effect, 4.0f));//s �� destroy
        }
    }



}
