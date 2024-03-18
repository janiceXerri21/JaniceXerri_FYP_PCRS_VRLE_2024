using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviornmentCustomisationState : MonoBehaviour
{
    public int deskDeco = 1;
    public int skyDeco = 1;
    public int skyColour = 1;
    public int floorColour = 1;
    
    public int ConfigNumber = 1111; //each possible combination of settings is given its own configNumber

    public bool startRandom = true;

    public Color[] skyColourOptions;
    public Color[] floorColourOptions;

    public GameObject[] closeDecorations;
    public GameObject[] farDecorations;


    private int ConfigAtStart = 1111;

    // Start is called before the first frame update
    void Start()
    {
        if(startRandom)
            randomiseState();
        ConfigAtStart = ConfigNumber;
    }

    // Update is called once per frame
    void Update()
    {
        int prevDeskDeco = deskDeco;
        int prevSkyDeco = skyDeco;

        //update the features to always match the configNumber
        char firstDigit = ConfigNumber.ToString()[0];
        deskDeco = int.Parse(firstDigit.ToString());
        char secondDigit = ConfigNumber.ToString()[1];
        skyColour = int.Parse(secondDigit.ToString());
        char thirdDigit = ConfigNumber.ToString()[2];
        skyDeco = int.Parse(thirdDigit.ToString());
        char forthDigit = ConfigNumber.ToString()[3];
        floorColour = int.Parse(forthDigit.ToString());

        //update the environment to match
        GameObject.Find("Main Camera").gameObject.GetComponent<Camera>().backgroundColor = skyColourOptions[skyColour - 1];

        GameObject.Find("Floor").gameObject.GetComponent<Renderer>().material.color = floorColourOptions[floorColour - 1];

        foreach (var obj in closeDecorations) obj.SetActive(false);
        closeDecorations[deskDeco-1].SetActive(true);

        foreach (var obj in farDecorations) obj.SetActive(false);
        farDecorations[skyDeco-1].SetActive(true);
    }

    //randomises the value for each feature and uses those to define the new random current state
    public void randomiseState()
    {
        //randomise the current state
        deskDeco = Random.Range(1, 5);
        skyColour = Random.Range(1, 5);
        skyDeco = Random.Range(1, 5);
        floorColour = Random.Range(1, 5);

        //update the config Number
        ConfigNumber = int.Parse(deskDeco.ToString() + skyDeco.ToString() + skyColour.ToString() + floorColour.ToString());
    }

    //Resets the config value to the starting config
    public void resetStateToStart()
    {
        //reset the config number (how unexpected)
        ConfigNumber = ConfigAtStart;
    }

    //converts a config number to a 2d position
    public int[] getCurrentPosition()
    {
        int[] position = new int[2];

        switch (ConfigNumber)
        {
            //row 1
            case 1111: position[0] = 1; position[1] = 1; break;
            case 2111: position[0] = 2; position[1] = 1; break;
            case 1211: position[0] = 3; position[1] = 1; break;
            case 1121: position[0] = 4; position[1] = 1; break;
            case 1112: position[0] = 5; position[1] = 1; break;
            case 1212: position[0] = 6; position[1] = 1; break;
            case 1221: position[0] = 7; position[1] = 1; break;
            case 1122: position[0] = 8; position[1] = 1; break;
            case 2211: position[0] = 9; position[1] = 1; break;
            case 2112: position[0] = 10; position[1] = 1; break;
            case 2121: position[0] = 11; position[1] = 1; break;
            case 2221: position[0] = 12; position[1] = 1; break;
            case 2212: position[0] = 13; position[1] = 1; break;
            case 2122: position[0] = 14; position[1] = 1; break;
            case 1222: position[0] = 15; position[1] = 1; break;
            case 2222: position[0] = 16; position[1] = 1; break;

            //row 2
            case 3111: position[0] = 1; position[1] = 2; break;
            case 4111: position[0] = 2; position[1] = 2; break;
            case 4211: position[0] = 3; position[1] = 2; break;
            case 4121: position[0] = 4; position[1] = 2; break;
            case 4112: position[0] = 5; position[1] = 2; break;
            case 4212: position[0] = 6; position[1] = 2; break;
            case 4221: position[0] = 7; position[1] = 2; break;
            case 2411: position[0] = 8; position[1] = 2; break;
            case 1322: position[0] = 9; position[1] = 2; break;
            case 3112: position[0] = 10; position[1] = 2; break;
            case 3121: position[0] = 11; position[1] = 2; break;
            case 3221: position[0] = 12; position[1] = 2; break;
            case 3212: position[0] = 13; position[1] = 2; break;
            case 3122: position[0] = 14; position[1] = 2; break;
            case 3222: position[0] = 15; position[1] = 2; break;
            case 4222: position[0] = 16; position[1] = 2; break;

            //row 3
            case 1311: position[0] = 1; position[1] = 3; break;
            case 4311: position[0] = 2; position[1] = 3; break;
            case 1411: position[0] = 3; position[1] = 3; break;
            case 1421: position[0] = 4; position[1] = 3; break;
            case 1412: position[0] = 5; position[1] = 3; break;
            case 1223: position[0] = 6; position[1] = 3; break;
            case 1123: position[0] = 7; position[1] = 3; break;
            case 1422: position[0] = 8; position[1] = 3; break;
            case 2311: position[0] = 9; position[1] = 3; break;
            case 2312: position[0] = 10; position[1] = 3; break;
            case 2321: position[0] = 11; position[1] = 3; break;
            case 2214: position[0] = 12; position[1] = 3; break;
            case 2114: position[0] = 13; position[1] = 3; break;
            case 2322: position[0] = 14; position[1] = 3; break;
            case 2422: position[0] = 15; position[1] = 3; break;
            case 3422: position[0] = 16; position[1] = 3; break;


            //row 4
            case 1131: position[0] = 1; position[1] = 4; break;
            case 4131: position[0] = 2; position[1] = 4; break;
            case 1431: position[0] = 3; position[1] = 4; break;
            case 1141: position[0] = 4; position[1] = 4; break;
            case 1142: position[0] = 5; position[1] = 4; break;
            case 1242: position[0] = 6; position[1] = 4; break;
            case 1241: position[0] = 7; position[1] = 4; break;
            case 1231: position[0] = 8; position[1] = 4; break;
            case 2231: position[0] = 9; position[1] = 4; break;
            case 2132: position[0] = 10; position[1] = 4; break;
            case 2131: position[0] = 11; position[1] = 4; break;
            case 2142: position[0] = 12; position[1] = 4; break;
            case 2232: position[0] = 13; position[1] = 4; break;
            case 2342: position[0] = 14; position[1] = 4; break;
            case 3242: position[0] = 15; position[1] = 4; break;
            case 2242: position[0] = 16; position[1] = 4; break;

            //row 5
            case 1113: position[0] = 1; position[1] = 5; break;
            case 4113: position[0] = 2; position[1] = 5; break;
            case 1413: position[0] = 3; position[1] = 5; break;
            case 1312: position[0] = 4; position[1] = 5; break;
            case 1114: position[0] = 5; position[1] = 5; break;
            case 1214: position[0] = 6; position[1] = 5; break;
            case 1224: position[0] = 7; position[1] = 5; break;
            case 1124: position[0] = 8; position[1] = 5; break;
            case 2213: position[0] = 9; position[1] = 5; break;
            case 2113: position[0] = 10; position[1] = 5; break;
            case 2123: position[0] = 11; position[1] = 5; break;
            case 2223: position[0] = 12; position[1] = 5; break;
            case 2421: position[0] = 13; position[1] = 5; break;
            case 2324: position[0] = 14; position[1] = 5; break;
            case 3224: position[0] = 15; position[1] = 5; break;
            case 2224: position[0] = 16; position[1] = 5; break;

            //row 6
            case 1313: position[0] = 1; position[1] = 6; break;
            case 1213: position[0] = 2; position[1] = 6; break;
            case 1321: position[0] = 3; position[1] = 6; break;
            case 1341: position[0] = 4; position[1] = 6; break;
            case 1314: position[0] = 5; position[1] = 6; break;
            case 1414: position[0] = 6; position[1] = 6; break;
            case 1243: position[0] = 7; position[1] = 6; break;
            case 1423: position[0] = 8; position[1] = 6; break;
            case 2314: position[0] = 9; position[1] = 6; break;
            case 2134: position[0] = 10; position[1] = 6; break;
            case 2323: position[0] = 11; position[1] = 6; break;
            case 2423: position[0] = 12; position[1] = 6; break;
            case 2432: position[0] = 13; position[1] = 6; break;
            case 2412: position[0] = 14; position[1] = 6; break;
            case 2124: position[0] = 15; position[1] = 6; break;
            case 2424: position[0] = 16; position[1] = 6; break;

            //row 7:
            case 1331: position[0] = 1; position[1] = 7; break;
            case 3114: position[0] = 2; position[1] = 7; break;
            case 1132: position[0] = 3; position[1] = 7; break;
            case 1143: position[0] = 4; position[1] = 7; break;
            case 1232: position[0] = 5; position[1] = 7; break;
            case 1342: position[0] = 6; position[1] = 7; break;
            case 1441: position[0] = 7; position[1] = 7; break;
            case 1234: position[0] = 8; position[1] = 7; break;
            case 2143: position[0] = 9; position[1] = 7; break;
            case 2332: position[0] = 10; position[1] = 7; break;
            case 2431: position[0] = 11; position[1] = 7; break;
            case 2141: position[0] = 12; position[1] = 7; break;
            case 2234: position[0] = 13; position[1] = 7; break;
            case 2241: position[0] = 14; position[1] = 7; break;
            case 4223: position[0] = 15; position[1] = 7; break;
            case 2442: position[0] = 16; position[1] = 7; break;

            //row 8
            case 1133: position[0] = 1; position[1] = 8; break;
            case 3411: position[0] = 2; position[1] = 8; break;
            case 1433: position[0] = 3; position[1] = 8; break;
            case 3141: position[0] = 4; position[1] = 8; break;
            case 1134: position[0] = 5; position[1] = 8; break;
            case 1432: position[0] = 6; position[1] = 8; break;
            case 1324: position[0] = 7; position[1] = 8; break;
            case 1144: position[0] = 8; position[1] = 8; break;
            case 2233: position[0] = 9; position[1] = 8; break;
            case 2413: position[0] = 10; position[1] = 8; break;
            case 2341: position[0] = 11; position[1] = 8; break;
            case 2243: position[0] = 12; position[1] = 8; break;
            case 4232: position[0] = 13; position[1] = 8; break;
            case 2344: position[0] = 14; position[1] = 8; break;
            case 4322: position[0] = 15; position[1] = 8; break;
            case 2244: position[0] = 16; position[1] = 8; break;

            //row 16
            case 3333: position[0] = 1; position[1] = 16; break;
            case 4333: position[0] = 2; position[1] = 16; break;
            case 3433: position[0] = 3; position[1] = 16; break;
            case 3343: position[0] = 4; position[1] = 16; break;
            case 3334: position[0] = 5; position[1] = 16; break;
            case 3434: position[0] = 6; position[1] = 16; break;
            case 3443: position[0] = 7; position[1] = 16; break;
            case 3344: position[0] = 8; position[1] = 16; break;
            case 4433: position[0] = 9; position[1] = 16; break;
            case 4334: position[0] = 10; position[1] = 16; break;
            case 4343: position[0] = 11; position[1] = 16; break;
            case 4443: position[0] = 12; position[1] = 16; break;
            case 4434: position[0] = 13; position[1] = 16; break;
            case 4344: position[0] = 14; position[1] = 16; break;
            case 3444: position[0] = 15; position[1] = 16; break;
            case 4444: position[0] = 16; position[1] = 16; break;

            //row 15
            case 1333: position[0] = 1; position[1] = 15; break;
            case 2333: position[0] = 2; position[1] = 15; break;
            case 2433: position[0] = 3; position[1] = 15; break;
            case 2343: position[0] = 4; position[1] = 15; break;
            case 2334: position[0] = 5; position[1] = 15; break;
            case 2434: position[0] = 6; position[1] = 15; break;
            case 2443: position[0] = 7; position[1] = 15; break;
            case 4233: position[0] = 8; position[1] = 15; break;
            case 3144: position[0] = 9; position[1] = 15; break;
            case 1334: position[0] = 10; position[1] = 15; break;
            case 1343: position[0] = 11; position[1] = 15; break;
            case 1443: position[0] = 12; position[1] = 15; break;
            case 1434: position[0] = 13; position[1] = 15; break;
            case 1344: position[0] = 14; position[1] = 15; break;
            case 1444: position[0] = 15; position[1] = 15; break;
            case 2444: position[0] = 16; position[1] = 15; break;

            //row 14
            case 3133: position[0] = 1; position[1] = 14; break;
            case 2133: position[0] = 2; position[1] = 14; break;
            case 3233: position[0] = 3; position[1] = 14; break;
            case 3441: position[0] = 4; position[1] = 14; break;
            case 3341: position[0] = 5; position[1] = 14; break;
            case 3234: position[0] = 6; position[1] = 14; break;
            case 3243: position[0] = 7; position[1] = 14; break;
            case 3244: position[0] = 8; position[1] = 14; break;
            case 4133: position[0] = 9; position[1] = 14; break;
            case 4134: position[0] = 10; position[1] = 14; break;
            case 4143: position[0] = 11; position[1] = 14; break;
            case 4432: position[0] = 12; position[1] = 14; break;
            case 4332: position[0] = 13; position[1] = 14; break;
            case 4144: position[0] = 14; position[1] = 14; break;
            case 1244: position[0] = 15; position[1] = 14; break;
            case 4244: position[0] = 16; position[1] = 14; break;

            //row 13
            case 3313: position[0] = 1; position[1] = 13; break;
            case 2313: position[0] = 2; position[1] = 13; break;
            case 3213: position[0] = 3; position[1] = 13; break;
            case 3323: position[0] = 4; position[1] = 13; break;
            case 3413: position[0] = 5; position[1] = 13; break;
            case 3424: position[0] = 6; position[1] = 13; break;
            case 3423: position[0] = 7; position[1] = 13; break;
            case 3324: position[0] = 8; position[1] = 13; break;
            case 4413: position[0] = 9; position[1] = 13; break;
            case 4314: position[0] = 10; position[1] = 13; break;
            case 4313: position[0] = 11; position[1] = 13; break;
            case 4324: position[0] = 12; position[1] = 13; break;
            case 4414: position[0] = 13; position[1] = 13; break;
            case 4124: position[0] = 14; position[1] = 13; break;
            case 1424: position[0] = 15; position[1] = 13; break;
            case 4424: position[0] = 16; position[1] = 13; break;

            //row 12
            case 3331: position[0] = 1; position[1] = 12; break;
            case 2331: position[0] = 2; position[1] = 12; break;
            case 3231: position[0] = 3; position[1] = 12; break;
            case 3134: position[0] = 4; position[1] = 12; break;
            case 3332: position[0] = 5; position[1] = 12; break;
            case 3432: position[0] = 6; position[1] = 12; break;
            case 3442: position[0] = 7; position[1] = 12; break;
            case 3342: position[0] = 8; position[1] = 12; break;
            case 4431: position[0] = 9; position[1] = 12; break;
            case 4331: position[0] = 10; position[1] = 12; break;
            case 4341: position[0] = 11; position[1] = 12; break;
            case 4441: position[0] = 12; position[1] = 12; break;
            case 4243: position[0] = 13; position[1] = 12; break;
            case 4142: position[0] = 14; position[1] = 12; break;
            case 1442: position[0] = 15; position[1] = 12; break;
            case 4442: position[0] = 16; position[1] = 12; break;

            //row 11
            case 3131: position[0] = 1; position[1] = 11; break;
            case 3431: position[0] = 2; position[1] = 11; break;
            case 3143: position[0] = 3; position[1] = 11; break;
            case 3123: position[0] = 4; position[1] = 11; break;
            case 3132: position[0] = 5; position[1] = 11; break;
            case 3232: position[0] = 6; position[1] = 11; break;
            case 3421: position[0] = 7; position[1] = 11; break;
            case 3241: position[0] = 8; position[1] = 11; break;
            case 4132: position[0] = 9; position[1] = 11; break;
            case 4312: position[0] = 10; position[1] = 11; break;
            case 4141: position[0] = 11; position[1] = 11; break;
            case 4241: position[0] = 12; position[1] = 11; break;
            case 4214: position[0] = 13; position[1] = 11; break;
            case 4234: position[0] = 14; position[1] = 11; break;
            case 4342: position[0] = 15; position[1] = 11; break;
            case 4242: position[0] = 16; position[1] = 11; break;

            //row 10
            case 3113: position[0] = 1; position[1] = 10; break;
            case 1332: position[0] = 2; position[1] = 10; break;
            case 3314: position[0] = 3; position[1] = 10; break;
            case 3321: position[0] = 4; position[1] = 10; break;
            case 3414: position[0] = 5; position[1] = 10; break;
            case 3124: position[0] = 6; position[1] = 10; break;
            case 3223: position[0] = 7; position[1] = 10; break;
            case 3412: position[0] = 8; position[1] = 10; break;
            case 4321: position[0] = 9; position[1] = 10; break;
            case 4114: position[0] = 10; position[1] = 10; break;
            case 4213: position[0] = 11; position[1] = 10; break;
            case 4323: position[0] = 12; position[1] = 10; break;
            case 4412: position[0] = 13; position[1] = 10; break;
            case 4423: position[0] = 14; position[1] = 10; break;
            case 2441: position[0] = 15; position[1] = 10; break;
            case 4224: position[0] = 16; position[1] = 10; break;

            //row 9
            case 3311: position[0] = 1; position[1] = 9; break;
            case 1233: position[0] = 2; position[1] = 9; break;
            case 3211: position[0] = 3; position[1] = 9; break;
            case 1323: position[0] = 4; position[1] = 9; break;
            case 3312: position[0] = 5; position[1] = 9; break;
            case 3214: position[0] = 6; position[1] = 9; break;
            case 3142: position[0] = 7; position[1] = 9; break;
            case 3322: position[0] = 8; position[1] = 9; break;
            case 4411: position[0] = 9; position[1] = 9; break;
            case 4231: position[0] = 10; position[1] = 9; break;
            case 4123: position[0] = 11; position[1] = 9; break;
            case 4421: position[0] = 12; position[1] = 9; break;
            case 2414: position[0] = 13; position[1] = 9; break;
            case 4122: position[0] = 14; position[1] = 9; break;
            case 2144: position[0] = 15; position[1] = 9; break;
            case 4422: position[0] = 16; position[1] = 9; break;
        }
        return position;
    }

    //converts a 2d position to a config number
    public int PositionToConfig(int x, int y)
    {
        int ConfigNumber = 1111;

        switch (x, y)
        {
            //row 1
            case (1, 1): ConfigNumber = 1111; break;
            case (2, 1): ConfigNumber = 2111; break;
            case (3, 1): ConfigNumber = 1211; break;
            case (4, 1): ConfigNumber = 1121; break;
            case (5, 1): ConfigNumber = 1112; break;
            case (6, 1): ConfigNumber = 1212; break;
            case (7, 1): ConfigNumber = 1221; break;
            case (8, 1): ConfigNumber = 1122; break;
            case (9, 1): ConfigNumber = 2211; break;
            case (10, 1): ConfigNumber = 2112; break;
            case (11, 1): ConfigNumber = 2121; break;
            case (12, 1): ConfigNumber = 2221; break;
            case (13, 1): ConfigNumber = 2212; break;
            case (14, 1): ConfigNumber = 2122; break;
            case (15, 1): ConfigNumber = 1222; break;
            case (16, 1): ConfigNumber = 2222; break;

            //row 2
            case (1, 2): ConfigNumber = 3111; break;
            case (2, 2): ConfigNumber = 4111; break;
            case (3, 2): ConfigNumber = 4211; break;
            case (4, 2): ConfigNumber = 4121; break;
            case (5, 2): ConfigNumber = 4112; break;
            case (6, 2): ConfigNumber = 4212; break;
            case (7, 2): ConfigNumber = 4221; break;
            case (8, 2): ConfigNumber = 2411; break;
            case (9, 2): ConfigNumber = 1322; break;
            case (10, 2): ConfigNumber = 3112; break;
            case (11, 2): ConfigNumber = 3121; break;
            case (12, 2): ConfigNumber = 3221; break;
            case (13, 2): ConfigNumber = 3212; break;
            case (14, 2): ConfigNumber = 3122; break;
            case (15, 2): ConfigNumber = 3222; break;
            case (16, 2): ConfigNumber = 4222; break;

            //row 3
            case (1, 3): ConfigNumber = 1311; break;
            case (2, 3): ConfigNumber = 4311; break;
            case (3, 3): ConfigNumber = 1411; break;
            case (4, 3): ConfigNumber = 1421; break;
            case (5, 3): ConfigNumber = 1412; break;
            case (6, 3): ConfigNumber = 1223; break;
            case (7, 3): ConfigNumber = 1123; break;
            case (8, 3): ConfigNumber = 1422; break;
            case (9, 3): ConfigNumber = 2311; break;
            case (10, 3): ConfigNumber = 2312; break;
            case (11, 3): ConfigNumber = 2321; break;
            case (12, 3): ConfigNumber = 2214; break;
            case (13, 3): ConfigNumber = 2114; break;
            case (14, 3): ConfigNumber = 2322; break;
            case (15, 3): ConfigNumber = 2422; break;
            case (16, 3): ConfigNumber = 3422; break;

            // Row 4
            case (1, 4): ConfigNumber = 1131; break;
            case (2, 4): ConfigNumber = 4131; break;
            case (3, 4): ConfigNumber = 1431; break;
            case (4, 4): ConfigNumber = 1141; break;
            case (5, 4): ConfigNumber = 1142; break;
            case (6, 4): ConfigNumber = 1242; break;
            case (7, 4): ConfigNumber = 1241; break;
            case (8, 4): ConfigNumber = 1231; break;
            case (9, 4): ConfigNumber = 2231; break;
            case (10, 4): ConfigNumber = 2132; break;
            case (11, 4): ConfigNumber = 2131; break;
            case (12, 4): ConfigNumber = 2142; break;
            case (13, 4): ConfigNumber = 2232; break;
            case (14, 4): ConfigNumber = 2342; break;
            case (15, 4): ConfigNumber = 3242; break;
            case (16, 4): ConfigNumber = 2242; break;

            // Row 5
            case (1, 5): ConfigNumber = 1113; break;
            case (2, 5): ConfigNumber = 4113; break;
            case (3, 5): ConfigNumber = 1413; break;
            case (4, 5): ConfigNumber = 1312; break;
            case (5, 5): ConfigNumber = 1114; break;
            case (6, 5): ConfigNumber = 1214; break;
            case (7, 5): ConfigNumber = 1224; break;
            case (8, 5): ConfigNumber = 1124; break;
            case (9, 5): ConfigNumber = 2213; break;
            case (10, 5): ConfigNumber = 2113; break;
            case (11, 5): ConfigNumber = 2123; break;
            case (12, 5): ConfigNumber = 2223; break;
            case (13, 5): ConfigNumber = 2421; break;
            case (14, 5): ConfigNumber = 2324; break;
            case (15, 5): ConfigNumber = 3224; break;
            case (16, 5): ConfigNumber = 2224; break;

            // Row 6
            case (1, 6): ConfigNumber = 1313; break;
            case (2, 6): ConfigNumber = 1213; break;
            case (3, 6): ConfigNumber = 1321; break;
            case (4, 6): ConfigNumber = 1341; break;
            case (5, 6): ConfigNumber = 1314; break;
            case (6, 6): ConfigNumber = 1414; break;
            case (7, 6): ConfigNumber = 1243; break;
            case (8, 6): ConfigNumber = 1423; break;
            case (9, 6): ConfigNumber = 2314; break;
            case (10, 6): ConfigNumber = 2134; break;
            case (11, 6): ConfigNumber = 2323; break;
            case (12, 6): ConfigNumber = 2423; break;
            case (13, 6): ConfigNumber = 2432; break;
            case (14, 6): ConfigNumber = 2412; break;
            case (15, 6): ConfigNumber = 2124; break;
            case (16, 6): ConfigNumber = 2424; break;

            // Row 7
            case (1, 7): ConfigNumber = 1331; break;
            case (2, 7): ConfigNumber = 3114; break;
            case (3, 7): ConfigNumber = 1132; break;
            case (4, 7): ConfigNumber = 1143; break;
            case (5, 7): ConfigNumber = 1232; break;
            case (6, 7): ConfigNumber = 1342; break;
            case (7, 7): ConfigNumber = 1441; break;
            case (8, 7): ConfigNumber = 1234; break;
            case (9, 7): ConfigNumber = 2143; break;
            case (10, 7): ConfigNumber = 2332; break;
            case (11, 7): ConfigNumber = 2431; break;
            case (12, 7): ConfigNumber = 2141; break;
            case (13, 7): ConfigNumber = 2234; break;
            case (14, 7): ConfigNumber = 2241; break;
            case (15, 7): ConfigNumber = 4223; break;
            case (16, 7): ConfigNumber = 2442; break;

            // Row 8
            case (1, 8): ConfigNumber = 1133; break;
            case (2, 8): ConfigNumber = 3411; break;
            case (3, 8): ConfigNumber = 1433; break;
            case (4, 8): ConfigNumber = 3141; break;
            case (5, 8): ConfigNumber = 1134; break;
            case (6, 8): ConfigNumber = 1432; break;
            case (7, 8): ConfigNumber = 1324; break;
            case (8, 8): ConfigNumber = 1144; break;
            case (9, 8): ConfigNumber = 2233; break;
            case (10, 8): ConfigNumber = 2413; break;
            case (11, 8): ConfigNumber = 2341; break;
            case (12, 8): ConfigNumber = 2243; break;
            case (13, 8): ConfigNumber = 4232; break;
            case (14, 8): ConfigNumber = 2344; break;
            case (15, 8): ConfigNumber = 4322; break;
            case (16, 8): ConfigNumber = 2244; break;

            // Row 9
            case (1, 9): ConfigNumber = 3311; break;
            case (2, 9): ConfigNumber = 1233; break;
            case (3, 9): ConfigNumber = 3211; break;
            case (4, 9): ConfigNumber = 1323; break;
            case (5, 9): ConfigNumber = 3312; break;
            case (6, 9): ConfigNumber = 3214; break;
            case (7, 9): ConfigNumber = 3142; break;
            case (8, 9): ConfigNumber = 3322; break;
            case (9, 9): ConfigNumber = 4411; break;
            case (10, 9): ConfigNumber = 4231; break;
            case (11, 9): ConfigNumber = 4123; break;
            case (12, 9): ConfigNumber = 4421; break;
            case (13, 9): ConfigNumber = 2414; break;
            case (14, 9): ConfigNumber = 4122; break;
            case (15, 9): ConfigNumber = 2144; break;
            case (16, 9): ConfigNumber = 4422; break;

            // Row 10
            case (1, 10): ConfigNumber = 3113; break;
            case (2, 10): ConfigNumber = 1332; break;
            case (3, 10): ConfigNumber = 3314; break;
            case (4, 10): ConfigNumber = 3321; break;
            case (5, 10): ConfigNumber = 3414; break;
            case (6, 10): ConfigNumber = 3124; break;
            case (7, 10): ConfigNumber = 3223; break;
            case (8, 10): ConfigNumber = 3412; break;
            case (9, 10): ConfigNumber = 4321; break;
            case (10, 10): ConfigNumber = 4114; break;
            case (11, 10): ConfigNumber = 4213; break;
            case (12, 10): ConfigNumber = 4323; break;
            case (13, 10): ConfigNumber = 4412; break;
            case (14, 10): ConfigNumber = 4423; break;
            case (15, 10): ConfigNumber = 2441; break;
            case (16, 10): ConfigNumber = 4224; break;

            // Row 11
            case (1, 11): ConfigNumber = 3131; break;
            case (2, 11): ConfigNumber = 3431; break;
            case (3, 11): ConfigNumber = 3143; break;
            case (4, 11): ConfigNumber = 3123; break;
            case (5, 11): ConfigNumber = 3132; break;
            case (6, 11): ConfigNumber = 3232; break;
            case (7, 11): ConfigNumber = 3421; break;
            case (8, 11): ConfigNumber = 3241; break;
            case (9, 11): ConfigNumber = 4132; break;
            case (10, 11): ConfigNumber = 4312; break;
            case (11, 11): ConfigNumber = 4141; break;
            case (12, 11): ConfigNumber = 4241; break;
            case (13, 11): ConfigNumber = 4214; break;
            case (14, 11): ConfigNumber = 4234; break;
            case (15, 11): ConfigNumber = 4342; break;
            case (16, 11): ConfigNumber = 4242; break;

            // Row 12
            case (1, 12): ConfigNumber = 3331; break;
            case (2, 12): ConfigNumber = 2331; break;
            case (3, 12): ConfigNumber = 3231; break;
            case (4, 12): ConfigNumber = 3134; break;
            case (5, 12): ConfigNumber = 3332; break;
            case (6, 12): ConfigNumber = 3432; break;
            case (7, 12): ConfigNumber = 3442; break;
            case (8, 12): ConfigNumber = 3342; break;
            case (9, 12): ConfigNumber = 4431; break;
            case (10, 12): ConfigNumber = 4331; break;
            case (11, 12): ConfigNumber = 4341; break;
            case (12, 12): ConfigNumber = 4441; break;
            case (13, 12): ConfigNumber = 4243; break;
            case (14, 12): ConfigNumber = 4142; break;
            case (15, 12): ConfigNumber = 1442; break;
            case (16, 12): ConfigNumber = 4442; break;

            // Row 13
            case (1, 13): ConfigNumber = 3313; break;
            case (2, 13): ConfigNumber = 2313; break;
            case (3, 13): ConfigNumber = 3213; break;
            case (4, 13): ConfigNumber = 3323; break;
            case (5, 13): ConfigNumber = 3413; break;
            case (6, 13): ConfigNumber = 3424; break;
            case (7, 13): ConfigNumber = 3423; break;
            case (8, 13): ConfigNumber = 3324; break;
            case (9, 13): ConfigNumber = 4413; break;
            case (10, 13): ConfigNumber = 4314; break;
            case (11, 13): ConfigNumber = 4313; break;
            case (12, 13): ConfigNumber = 4324; break;
            case (13, 13): ConfigNumber = 4414; break;
            case (14, 13): ConfigNumber = 4124; break;
            case (15, 13): ConfigNumber = 1424; break;
            case (16, 13): ConfigNumber = 4424; break;

            // Row 14
            case (1, 14): ConfigNumber = 3133; break;
            case (2, 14): ConfigNumber = 2133; break;
            case (3, 14): ConfigNumber = 3233; break;
            case (4, 14): ConfigNumber = 3441; break;
            case (5, 14): ConfigNumber = 3341; break;
            case (6, 14): ConfigNumber = 3234; break;
            case (7, 14): ConfigNumber = 3243; break;
            case (8, 14): ConfigNumber = 3244; break;
            case (9, 14): ConfigNumber = 4133; break;
            case (10, 14): ConfigNumber = 4134; break;
            case (11, 14): ConfigNumber = 4143; break;
            case (12, 14): ConfigNumber = 4432; break;
            case (13, 14): ConfigNumber = 4332; break;
            case (14, 14): ConfigNumber = 4144; break;
            case (15, 14): ConfigNumber = 1244; break;
            case (16, 14): ConfigNumber = 4244; break;

            // Row 15
            case (1, 15): ConfigNumber = 1333; break;
            case (2, 15): ConfigNumber = 2333; break;
            case (3, 15): ConfigNumber = 2433; break;
            case (4, 15): ConfigNumber = 2343; break;
            case (5, 15): ConfigNumber = 2334; break;
            case (6, 15): ConfigNumber = 2434; break;
            case (7, 15): ConfigNumber = 2443; break;
            case (8, 15): ConfigNumber = 4233; break;
            case (9, 15): ConfigNumber = 3144; break;
            case (10, 15): ConfigNumber = 1334; break;
            case (11, 15): ConfigNumber = 1343; break;
            case (12, 15): ConfigNumber = 1443; break;
            case (13, 15): ConfigNumber = 1434; break;
            case (14, 15): ConfigNumber = 1344; break;
            case (15, 15): ConfigNumber = 1444; break;
            case (16, 15): ConfigNumber = 2444; break;

            // Row 16
            case (1, 16): ConfigNumber = 3333; break;
            case (2, 16): ConfigNumber = 4333; break;
            case (3, 16): ConfigNumber = 3433; break;
            case (4, 16): ConfigNumber = 3343; break;
            case (5, 16): ConfigNumber = 3334; break;
            case (6, 16): ConfigNumber = 3434; break;
            case (7, 16): ConfigNumber = 3443; break;
            case (8, 16): ConfigNumber = 3344; break;
            case (9, 16): ConfigNumber = 4433; break;
            case (10, 16): ConfigNumber = 4334; break;
            case (11, 16): ConfigNumber = 4343; break;
            case (12, 16): ConfigNumber = 4443; break;
            case (13, 16): ConfigNumber = 4434; break;
            case (14, 16): ConfigNumber = 4344; break;
            case (15, 16): ConfigNumber = 3444; break;
            case (16, 16): ConfigNumber = 4444; break;
        }

        return ConfigNumber;
    }

}
